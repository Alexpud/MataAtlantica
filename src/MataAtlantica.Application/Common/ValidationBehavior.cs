using FluentValidation;
using MataAtlantica.Domain.Helpers;
using MediatR;

namespace MataAtlantica.Application.Common;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TResponse : BaseResponse, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        if (request is BaseCommand requisicao)
        {
            var hasErrors = requisicao.Validate().Errors.Any();
            if (hasErrors)
                return (TResponse)new BaseResponse()
                {
                    Errors = requisicao.Validate().GetErrors()  
                };
        }
        
        return await next();
    }
}