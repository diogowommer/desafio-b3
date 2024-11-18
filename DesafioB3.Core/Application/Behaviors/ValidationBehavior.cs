using DesafioB3.Core.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace DesafioB3.Core.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationTasks = _validators.Select(validator => validator.ValidateAsync(request, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks).ConfigureAwait(continueOnCapturedContext: false);

        var validationFailures = validationResults.SelectMany(result => result.Errors);

        if (!validationFailures.Any())
        {
            return await next().ConfigureAwait(continueOnCapturedContext: false);
        }

        var errors = validationFailures.Select(failure => new ValidationFailedException.ValidationError(failure.PropertyName, failure.ErrorMessage));

        throw new ValidationFailedException(errors);
    }
}