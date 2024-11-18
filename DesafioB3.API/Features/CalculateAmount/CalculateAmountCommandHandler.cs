using MediatR;
using System.Diagnostics.Metrics;

namespace DesafioB3.API.Features.CalculateAmount;

public class CalculateAmountCommandHandler : IRequestHandler<CalculateAmountCommand, CalculateAmountResponse>
{
    //CDI – 0,9%
    private const decimal CDI_RATE = 0.009m;     
    
    //TB – 108%
    private const decimal BANK_RATE = 1.08m;

    // Tax rates by period
    // Up to 6 months
    private const decimal SHORT_TERM_TAX = 0.225m;
    // Up to 12 months
    private const decimal MEDIUM_TERM_TAX = 0.20m;
    // Up to 24 months
    private const decimal LONG_TERM_TAX = 0.175m;
    // Above 24 months
    private const decimal EXTENDED_TERM_TAX = 0.15m;

    public CalculateAmountCommandHandler()
    {
    }

    public static decimal CalculateGrossValue(decimal initialValue, int months)
    {
        //VF = VI x [1 + (CDI x TB)]
        return Enumerable
            .Range(0, months)
            .Aggregate(initialValue, (currentValue, _) => currentValue * (1 + (CDI_RATE * BANK_RATE)));
    }

    public static decimal DetermineTaxRate(int months) =>
        months switch
        {
            <= 6 => SHORT_TERM_TAX,
            <= 12 => MEDIUM_TERM_TAX,
            <= 24 => LONG_TERM_TAX,
            _ => EXTENDED_TERM_TAX
        };

    private static decimal CalculateNetValue(decimal grossValue, decimal initialValue, decimal taxRate)
    {
        decimal profit = grossValue - initialValue;
        decimal taxAmount = profit * taxRate;
        return grossValue - taxAmount;
    }

    public Task<CalculateAmountResponse> Handle(CalculateAmountCommand request, CancellationToken cancellationToken)
    {
        decimal grossValue = CalculateGrossValue(request.InitialValue, request.Quantity);
        decimal taxRate = DetermineTaxRate(request.Quantity);
        decimal netValue = CalculateNetValue(grossValue, request.InitialValue, taxRate);

        return Task.FromResult(new CalculateAmountResponse(grossValue, netValue));
    }
}