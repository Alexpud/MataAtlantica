using FluentValidation;
using MediatR;

namespace MataAtlantica.Application.Common;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var validationResult = validationFailures.FirstOrDefault(validationResult => !validationResult.IsValid);
        if (validationResult != null)
            throw new ValidationException(validationResult.Errors);
        
        return await next();
    }
}