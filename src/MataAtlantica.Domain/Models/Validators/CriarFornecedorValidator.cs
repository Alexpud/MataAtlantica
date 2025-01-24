using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Fornecedores;

namespace MataAtlantica.Domain.Models.Validators;

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
