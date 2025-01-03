using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class CustomerCreator
{
    public static Customer Create(
        int id = 1,
        int countryId = 1,
        int stateProvinceId = 1,
        int billingAddressId = 1,
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
        email ??= "test@test.hu";
        firstName ??= "John";
        lastName ??= "Doe";
        streetAddress ??= "Main street 1";
        streetAddress2 ??= "Apt. 1";
        zipPostalCode ??= "1234";
        city ??= "New York";
        phone ??= "123456789";
        company ??= "Company";
        return new Customer
        {
            Id = id,
            CustomerGuid = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            StreetAddress = streetAddress,
            StreetAddress2 = streetAddress2,
            ZipPostalCode = zipPostalCode,
            City = city,
            Phone = phone,
            CountryId = countryId,
            StateProvinceId = stateProvinceId,
            Company = company,
            BillingAddressId = billingAddressId   
        };
    }
}
