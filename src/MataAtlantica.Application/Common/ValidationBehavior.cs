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
            var errors = requisicao.Validate().GetErrors();
            if (errors.Any())
            {
                var response = new TResponse();
                response.WithErrors(errors);
                return response;
            }
        }
        
        return await next();
    }
}