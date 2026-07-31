using Nop.Core.Domain.Directory;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class CountryCreator
{
    public static Country Create(int id = 1)
    {
        return new Country
        {
            Id = id,
            Name = "Hungary",
            TwoLetterIsoCode = "HU",
            ThreeLetterIsoCode = "HUN",
            NumericIsoCode = 348,
            SubjectToVat = true,
            LimitedToStores = false,
            DisplayOrder = 1
        };
    }
}
