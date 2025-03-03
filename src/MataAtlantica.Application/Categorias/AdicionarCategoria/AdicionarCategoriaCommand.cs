using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Categorias;
using MediatR;

namespace MataAtlantica.Application.Categorias.AdicionarCategoria;

public record AdicionarCategoriaCommand(string Nome) : BaseCommand, IRequest<CommandResponse<CategoriaDto>> 
{
    public override ValidationResult Validate()
    {
        var validator = new AdicionarCategoriaCommandValidator();
        return validator.Validate(this);
    }
}

public class AdicionarCategoriaCommandValidator : AbstractValidator<AdicionarCategoriaCommand>
{
    public AdicionarCategoriaCommandValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaCategoria))
            .WithMessage(nameof(EntityValidationErrors.NomeObrigatorioParaCategoria.Message));
    }
}
