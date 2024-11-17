using FluentValidation;

namespace DesafioB3.API.Features.CalculateAmount;

public class CalculateAmountCommandValidator : AbstractValidator<CalculateAmountCommand>
{
    public CalculateAmountCommandValidator()
    {
        RuleFor(command => command.InitialValue)
            .NotNull()
            .GreaterThan(0);

        RuleFor(command => command.Quantity)
            .NotNull()
            .GreaterThan(1);
    }
}