using Microsoft.AspNetCore.Http;

namespace DesafioB3.Core.Domain;

public interface IExceptionHandler
{
    Task HandleAsync(HttpContext context, Exception exception);
}