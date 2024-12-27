using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Catalog;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.SimplePay.Transactions;
public class SimplePayStartRequest
{
    private readonly SimplePaySettings _settings;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public SimplePayStartRequest(
        SimplePaySettings settings,
        IOrderService orderService,
        IProductService productService
        )
    {
        _settings = settings;
        _orderService = orderService;
        _productService = productService;
    }
    public async Task<StartRequest> CreateStartRequest(int orderId)
    {
        var orderItems = await _orderService.GetOrderItemsAsync(orderId);
        return new StartRequest
        {
            Merchant = _settings.MerchantKey,
            Items = await CreateItems(orderItems)
        };
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
