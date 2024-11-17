using FluentValidation;

namespace DesafioB3.API.Features.CalculateAmount;

public class CalculateAmountCommandValidator : AbstractValidator<CalculateAmountCommand>
{
    public CalculateAmountCommandValidator()
    {
        RuleFor(command => command.InitialValue)
            .NotNull()
            .NotEmpty()
            .GreaterThan(1);

        RuleFor(command => command.Quantity)
            .NotNull()
            .NotEmpty()
            .GreaterThan(1);
    }
}