using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
public static class CustomerAndAddressProvider
{
    public static int CustomerId => 1;
    public static int CustomerWithoutBillingAddressId => 2;
    public static Customer Customer { get; set; }
    public static Customer CustomerWithoutBillingAddress { get; set; }
    public static Address BillingAddress { get; set; }
    public static Country Country { get; set; }
    public static StateProvince StateProvince { get; set; }

    public static string CustomerFullName => $"{Customer.LastName} {Customer.FirstName}";
    public static string BillingAddressFullName => $"{BillingAddress.LastName} {BillingAddress.FirstName}";

    public static void Initialize()
    {
        Country = CountryCreator.Create();
        StateProvince = StateCreator.Create(countryId: Country.Id);
        BillingAddress = AddressCreator.Create(countryId: Country.Id, stateProvinceId: StateProvince.Id);
        Customer = CustomerCreator.Create(
            id: CustomerId,
            countryId: Country.Id, 
            stateProvinceId: StateProvince.Id, 
            billingAddressId: BillingAddress.Id);
        CustomerWithoutBillingAddress = CustomerCreator.Create(
            id: CustomerWithoutBillingAddressId, 
            countryId: Country.Id, 
            stateProvinceId: StateProvince.Id);
    }
}
