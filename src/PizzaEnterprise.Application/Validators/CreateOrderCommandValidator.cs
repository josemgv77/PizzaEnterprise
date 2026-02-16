using FluentValidation;
using PizzaEnterprise.Application.Commands.Orders;

namespace PizzaEnterprise.Application.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId must not be empty");

        RuleFor(x => x.DeliveryAddress)
            .NotNull()
            .WithMessage("DeliveryAddress must not be null");

        When(x => x.DeliveryAddress != null, () =>
        {
            RuleFor(x => x.DeliveryAddress.Street)
                .NotEmpty()
                .WithMessage("Street must not be empty");

            RuleFor(x => x.DeliveryAddress.City)
                .NotEmpty()
                .WithMessage("City must not be empty");

            RuleFor(x => x.DeliveryAddress.State)
                .NotEmpty()
                .WithMessage("State must not be empty");

            RuleFor(x => x.DeliveryAddress.ZipCode)
                .NotEmpty()
                .WithMessage("ZipCode must not be empty");

            RuleFor(x => x.DeliveryAddress.Country)
                .NotEmpty()
                .WithMessage("Country must not be empty");
        });

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Items must not be empty");

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.PizzaId)
                    .NotEmpty()
                    .WithMessage("PizzaId must not be empty");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0");
            });
    }
}
