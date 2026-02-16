using PizzaEnterprise.Domain.Common;
using PizzaEnterprise.Domain.Exceptions;

namespace PizzaEnterprise.Domain.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency = "USD")
    {
        if (amount < 0)
        {
            throw new DomainException("Amount cannot be negative");
        }

        return new Money(amount, currency);
    }

    public static Money Zero => Create(0);

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new DomainException($"Cannot add money with different currencies: {Currency} and {other.Currency}");
        }

        return Create(Amount + other.Amount, Currency);
    }

    public Money Multiply(int factor)
    {
        return Create(Amount * factor, Currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
