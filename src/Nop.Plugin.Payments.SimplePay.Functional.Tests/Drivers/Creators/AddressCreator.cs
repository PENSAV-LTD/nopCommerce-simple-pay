using Nop.Core.Domain.Common;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class AddressCreator
{
    public static Address Create(
        int id = 1,
        int countryId = 1,
        int stateProvinceId = 1,
        string email = null,
        string firstName = null,
        string lastName = null,
        string streetAddress = null,
        string streetAddress2 = null,
        string zipPostalCode = null,
        string city = null,
        string phone = null,
        string company = null
        )
    {
        email ??= "billing.test@test.hu";
        firstName ??= "Billing John";
        lastName ??= "Doe";
        streetAddress ??= "Billing Main street 1";
        streetAddress2 ??= "Biling Apt. 1";
        zipPostalCode ??= "B1234";
        city ??= "Billing New York";
        phone ??= "B123456789";
        company ??= "Billing Company";
        return new Address
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Address1 = streetAddress,
            Address2 = streetAddress2,
            ZipPostalCode = zipPostalCode,
            City = city,
            PhoneNumber = phone,
            CountryId = countryId,
            StateProvinceId = stateProvinceId,
            Company = company,
        };
    }
}
