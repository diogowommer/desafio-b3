using DesafioB3.Core.Application.Exceptions;
using DesafioB3.Core.Domain;
using System.Net;

namespace DesafioB3.API.Infrastructure.Handlers;

public class ExceptionHandler : IExceptionHandler
{
    public async Task HandleAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case ValidationFailedException validationFailedException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(validationFailedException.Errors);
                return;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new { Error = "Unexpected error. Please contact the support." });
                return;
        }
    }
}                   