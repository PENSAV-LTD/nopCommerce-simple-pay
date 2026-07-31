using Nop.Core.Domain.Directory;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class StateCreator
{
    public static StateProvince Create(
        int id = 1,
        int countryId = 1
        )
    {
        return new StateProvince
        {
            Id = id,
            CountryId = countryId,
            Name = "Budapest",
            Abbreviation = "BUD",
            DisplayOrder = 1
        };
    }
}
