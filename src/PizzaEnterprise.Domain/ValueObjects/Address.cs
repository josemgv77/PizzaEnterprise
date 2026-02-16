using PizzaEnterprise.Domain.Common;
using PizzaEnterprise.Domain.Exceptions;

namespace PizzaEnterprise.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public string Country { get; private set; }

    private Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public static Address Create(string street, string city, string state, string zipCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new DomainException("Street cannot be empty");

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City cannot be empty");

        if (string.IsNullOrWhiteSpace(state))
            throw new DomainException("State cannot be empty");

        if (string.IsNullOrWhiteSpace(zipCode))
            throw new DomainException("ZipCode cannot be empty");

        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country cannot be empty");

        return new Address(street, city, state, zipCode, country);
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State} {ZipCode}, {Country}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return ZipCode;
        yield return Country;
    }
}
