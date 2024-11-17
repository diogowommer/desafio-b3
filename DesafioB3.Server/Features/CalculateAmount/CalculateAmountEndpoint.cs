using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioB3.API.Features.CalculateAmount;

public static class CalculateAmountEndpoint
{
    public static async Task<IResult> PostAsync(
        [FromServices] IMediator mediator,
        [FromBody] CalculateAmountRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CalculateAmountCommand(request.InitialValue, request.Quantity);

        var response = await mediator.Send(command, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

        return Results.Ok(response);
    }
}