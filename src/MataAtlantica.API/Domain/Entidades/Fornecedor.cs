using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.API.Domain.Erros;

namespace MataAtlantica.API.Domain.Entidades;

public class Fornecedor : EntidadeBase
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Endereco Endereco { get; set; }
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public List<Produto> Produtos { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; }
    
    public override ValidationResult Validar() 
        => new FornecedorValidation().Validate(this);
}

public class Endereco
{
    public string Rua { get; set; }
    public string Bairro { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
}

public class FornecedorValidation : AbstractValidator<Fornecedor>
{
    public FornecedorValidation()
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
            .WithMessage(EntityValidationErrors.CpfCnpjObrigatorioParaFornecedor.Message);;
        
        RuleFor(p => p.Telefone)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.TelefoneObrigatorioParaFornecedor))
            .WithMessage(EntityValidationErrors.TelefoneObrigatorioParaFornecedor.Message);

        RuleFor(p => p.Endereco).SetValidator(new EnderecoValidator());

    }
}

public class EnderecoValidator : AbstractValidator<Endereco>
{
    public EnderecoValidator()
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
        
        RuleFor(p => p.Estado)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.EstadoObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.EstadoObrigatorioParaEndereco.Message);
        
        RuleFor(p => p.CEP)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.CepObrigatorioParaEndereco))
            .WithMessage(EntityValidationErrors.CepObrigatorioParaEndereco.Message);;
    }
}
