using MediatR;
using System.Diagnostics.Metrics;

namespace DesafioB3.API.Features.CalculateAmount;

public class CalculateAmountCommandHandler : IRequestHandler<CalculateAmountCommand, CalculateAmountResponse>
{
    //CDI – 0,9%
    private const decimal CDI_RATE = 0.009m;     
    
    //TB – 108%
    private const decimal BANK_RATE = 1.08m;    

    public CalculateAmountCommandHandler()
    {
    }

    public static decimal CalculateFinalValue(decimal initialValue, int months)
    {
        decimal finalValue = initialValue;

        for (int i = 0; i < months; i++)
        {
            //VF = VI x [1 + (CDI x TB)]
            finalValue *= (1 + (CDI_RATE * BANK_RATE));
        }

        return finalValue;
    }

    private static decimal GetTaxRate(int months)
    {
        //Até 06 meses: 22,5%
        if (months <= 6)
        {
            return 0.225m;  
        }
        //Até 12 meses: 20%
        else if (months <= 12)
        {
            return 0.20m;
        }
        //Até 24 meses 17,5%
        else if (months <= 24)
        {
            return 0.175m;
        }
        //Acima de 24 meses 15%
        else
        {
            return 0.15m;
        }
    }

    public Task<CalculateAmountResponse> Handle(CalculateAmountCommand request, CancellationToken cancellationToken)
    {
        decimal grossValue = CalculateFinalValue(request.InitialValue, request.Quantity);

        decimal taxRate = GetTaxRate(request.Quantity);

        decimal profit = grossValue - request.InitialValue;

        decimal taxAmount = profit * taxRate;

        decimal netValue = grossValue - taxAmount;

        return Task.FromResult(new CalculateAmountResponse(grossValue, netValue));
    }
}