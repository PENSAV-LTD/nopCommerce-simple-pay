using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
public static class CustomerAndAddressProvider
{
    public static Customer Customer { get; set; }
    public static Address BillingAddress { get; set; }
    public static Country Country { get; set; }
    public static StateProvince StateProvince { get; set; }

    public static void Initialize()
    {
        Country = CountryCreator.Create();
        StateProvince = StateCreator.Create(countryId: Country.Id);
        BillingAddress = AddressCreator.Create(countryId: Country.Id, stateProvinceId: StateProvince.Id);
        Customer = CustomerCreator.Create(countryId: Country.Id, stateProvinceId: StateProvince.Id, billingAddressId: BillingAddress.Id);
    }

}
