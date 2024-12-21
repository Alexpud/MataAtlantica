using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models;

namespace MataAtlantica.Domain.Models.Validators;

public class AlterarFornecedorValidator : AbstractValidator<AlterarFornecedorDto>
{
    public AlterarFornecedorValidator(IFornecedorRepository fornecedorRepository)
    {
        RuleFor(p => p.Id)
            .MustAsync(async (id, token) => await fornecedorRepository.ObterPorId(id) != null)
            .WithMessage(BusinessErrors.FornecedorNaoEncontrado.Message)
            .WithErrorCode(nameof(BusinessErrors.FornecedorNaoEncontrado));

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
            .MustAsync(async (model, cpfCnpj, token) =>
            {
                var fornecedor = await fornecedorRepository.ObterPorCpfCnpj(cpfCnpj);
                return fornecedor == null || fornecedor.Id == model.Id;
            })
            .WithMessage(BusinessErrors.FornecedorComCpfCnpjJaExiste.Message)
            .WithErrorCode(nameof(BusinessErrors.FornecedorComCpfCnpjJaExiste));


        RuleFor(p => p.Telefone)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.TelefoneObrigatorioParaFornecedor))
            .WithMessage(EntityValidationErrors.TelefoneObrigatorioParaFornecedor.Message);

        RuleFor(p => p.Localizacao).SetValidator(new EnderecoFornecedorValidator());
    }
}
