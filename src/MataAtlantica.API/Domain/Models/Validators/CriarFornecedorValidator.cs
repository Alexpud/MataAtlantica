using FluentValidation;
using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Erros;

namespace MataAtlantica.API.Domain.Models.Validators;

public class CriarFornecedorValidator : AbstractValidator<AdicionarFornecedorDto>
{
    public CriarFornecedorValidator(IFornecedorRepository fornecedorRepository)
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaFornecedor))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaFornecedor.Message);

        RuleFor(p => p.Descricao)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.DescricaoObrigatoriaParaFornecedor))
            .WithMessage(EntityValidationErrors.DescricaoObrigatoriaParaFornecedor.Message);

        RuleFor(p => p.CpfCnpj)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.CpfCnpjObrigatorioParaFornecedor))
            .WithMessage(EntityValidationErrors.CpfCnpjObrigatorioParaFornecedor.Message)
            .MustAsync(async (cpfCnpj, token) => await fornecedorRepository.ObterPorCpfCnpj(cpfCnpj) == null)
            .WithMessage(BusinessErrors.FornecedorComCpfCnpjJaExiste.Message)
            .WithErrorCode(nameof(BusinessErrors.FornecedorComCpfCnpjJaExiste));


        RuleFor(p => p.Telefone)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.TelefoneObrigatorioParaFornecedor))
            .WithMessage(EntityValidationErrors.TelefoneObrigatorioParaFornecedor.Message);

        RuleFor(p => p.Localizacao).SetValidator(new EnderecoFornecedorValidator());

    }
}

public class EnderecoFornecedorValidator : AbstractValidator<EnderecoFornecedor>
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
