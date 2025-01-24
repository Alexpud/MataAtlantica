using FluentValidation;
using MataAtlantica.Domain.Erros;

namespace MataAtlantica.Domain.Models.Validators;

public class EnderecoFornecedorValidator : AbstractValidator<Endereco>
{
    public EnderecoFornecedorValidator()
    {
        RuleFor(p => p.Rua)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.RuaObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.RuaObrigatorioParaEndereco.Message);

        RuleFor(p => p.Bairro)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.BairroObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.BairroObrigatorioParaEndereco.Message);

        RuleFor(p => p.Numero)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NumeroObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.NumeroObrigatorioParaEndereco.Message);

        RuleFor(p => p.Cidade)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.CidadeObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.CidadeObrigatorioParaEndereco.Message);

        RuleFor(p => p.UF)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.EstadoObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.EstadoObrigatorioParaEndereco.Message)
            .MaximumLength(2)
            .WithErrorCode(nameof(EntityValidationErrors.UFDeveTerNoMaximo2CaracteresParaEndereco))
            .WithMessage(EntityValidationErrors.UFDeveTerNoMaximo2CaracteresParaEndereco.Message);

        RuleFor(p => p.CEP)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.CepObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.CepObrigatorioParaEndereco.Message); ;
    }
}
