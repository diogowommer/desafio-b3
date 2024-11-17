using MediatR;

namespace DesafioB3.API.Features.CalculateAmount;

public class CalculateAmountCommandHandler : IRequestHandler<CalculateAmountCommand, CalculateAmountResponse>
{
    public CalculateAmountCommandHandler()
    {
    }

    public Task<CalculateAmountResponse> Handle(CalculateAmountCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CalculateAmountResponse(123, 123));
    }
}