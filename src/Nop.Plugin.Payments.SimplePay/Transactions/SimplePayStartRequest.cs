using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Exceptions;
using Nop.Plugin.Payments.SimplePay.Messages.Generators;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.SimplePay.Transactions;
public class SimplePayStartRequest
{
    private readonly SimplePaySettings _settings;
    private readonly ISaltGenerator _saltGenerator;
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;
    private readonly ICountryService _countryService;
    private readonly IStateProvinceService _stateProvinceService;
    private readonly IProductService _productService;

    public SimplePayStartRequest(
        SimplePaySettings settings,
        ISaltGenerator saltGenerator,
        IOrderService orderService,
        ICustomerService customerService,
        IAddressService addressService,
        ICountryService countryService,
        IStateProvinceService stateProvinceService,
        IProductService productService
        )
    {
        _settings = settings;
        _saltGenerator = saltGenerator;
        _orderService = orderService;
        _customerService = customerService;
        _addressService = addressService;
        _countryService = countryService;
        _stateProvinceService = stateProvinceService;
        _productService = productService;
    }
    public async Task<StartRequest> CreateStartRequestAsync(Order order)
    {
        var orderItems = await _orderService.GetOrderItemsAsync(order.Id);
        var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);
        return new StartRequest
        {
            Salt = _saltGenerator.Generate(),
            OrderRef = order.Id.ToString(),
            Total = Convert.ToInt32(order.OrderTotal),
            Currency = _settings.IsDefaultCurrencyUsed ? _settings.DefaultCurrency : order.CustomerCurrencyCode,
            Merchant = _settings.MerchantKey,
            Items = await CreateItems(orderItems),
            CustomerEmail = customer.Email,
            Invoice = await CreateInvoiceAsync(customer),
        };
    }

    private async Task<InvoiceDetail> CreateInvoiceAsync(Customer customer)
    {
        Address address = null;
        if (customer.BillingAddressId.GetValueOrDefault() != 0)
        {
            address = await _addressService.GetAddressByIdAsync(customer.BillingAddressId.Value);
        }
        var country = await GetAndCheckCountryAsync(address?.CountryId ?? customer.CountryId);
        var stateProvince = await GetAndCheckStateProvinceAsync(address?.StateProvinceId ?? customer.StateProvinceId);

        return new InvoiceDetail
        {
            Name = address != null ? $"{address.LastName} {address.FirstName}" : $"{customer.LastName} {customer.FirstName}",
            Company = address?.Company ?? customer.Company,
            Phone = address?.PhoneNumber ?? customer.Phone,
            Country = country.TwoLetterIsoCode,
            State = stateProvince.Name,
            City = address?.City ?? customer.City,
            Address = address?.Address1 ?? customer.StreetAddress,
            Address2 = address?.Address2 ?? customer.StreetAddress2,
            Zip = address?.ZipPostalCode ?? customer.ZipPostalCode,
            ThreeDSReqAuthMethod = ThreeDSReqAuthMethod.Registered,
        };
    }

    private async Task<StateProvince> GetAndCheckStateProvinceAsync(int stateProvinceId)
    {
        var stateProvince = await _stateProvinceService.GetStateProvinceByIdAsync(stateProvinceId);

        if (stateProvince == null)
            throw new SimplePayStateProvinceNotFoundException();
        if (string.IsNullOrWhiteSpace(stateProvince.Name))
            throw new SimplePayStateProvinceNameIsEmptyException();
        return stateProvince;
    }

    private async Task<Country> GetAndCheckCountryAsync(int countryId)
    {
        var country = await _countryService.GetCountryByIdAsync(countryId);
        if (country == null)
            throw new SimplePayCountryNotFoundException();
        if (string.IsNullOrWhiteSpace(country.TwoLetterIsoCode))
            throw new SimplePayCountryTwoLetterIsoCodeEmptyException();
        return country;
    }

    private async Task<List<StartRequestItem>> CreateItems(IList<OrderItem> orderItems)
    {
        var items = new List<StartRequestItem>();
        foreach (var orderItem in orderItems)
        {
            var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
            items.Add(new StartRequestItem
            {
                Title = product.Name,
                Amount = orderItem.Quantity,
                Price = orderItem.PriceInclTax,
                Tax = 0,
            });
        }
        return items;
    }
}
