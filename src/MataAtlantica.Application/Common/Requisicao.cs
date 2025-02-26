
using FluentValidation.Results;
using MediatR;

namespace MataAtlantica.Application.Common;

public class Requisicao : BaseRequest, IRequest<string>
{
    public override ValidationResult Validate()
    {
        return new ValidationResult()
        {
            Errors = new List<ValidationFailure>() { new ValidationFailure("Nome Completo", "Nome Completo") }
        };
    }
}