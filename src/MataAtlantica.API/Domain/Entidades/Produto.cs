using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.API.Domain.Erros;

namespace MataAtlantica.API.Domain.Entidades;

public class Produto : EntidadeBase
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public float Preco { get; set; }
    public ConfiguracaoImagens ConfiguracaoImagens { get; private set; }
    public Categoria Categoria { get; set; }
    public string CategoriaId { get; set; }
    public Fornecedor Fornecedor { get; set; }
    public string FornecedorId { get; set; }
    public DateTime? UltimaAtualizacao { get; private set; }

    public void AdicionarImagemThumbnail(string nome, int ordem)
        => ConfiguracaoImagens.Thumbnails.Add(new Imagem(nome, ordem));

    public void AdicionarImagemIlustrativa(string nome, int ordem)
        =>ConfiguracaoImagens.ImagensIlustrativas.Add(new Imagem(nome, ordem));
}

public class ConfiguracaoImagens
{
    public List<Imagem> Thumbnails { get; set; }
    public List<Imagem> ImagensIlustrativas { get; set; }
}

public class Imagem(string Nome, int Ordem)
{
    public string Nome { get; set; } = Nome;
    public int Ordem { get; set; } = Ordem;
}

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(produto => produto.Nome)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.NomeObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.NomeObrigatorioParaProduto.Message);

        RuleFor(produto => produto.Descricao)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.DescricaoObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.DescricaoObrigatorioParaProduto.Message);

        RuleFor(produto => produto.Marca)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.MarcaObrigatoriaParaProduto))
            .WithMessage(EntityValidationErrors.MarcaObrigatoriaParaProduto.Message);

        RuleFor(produto => produto.Preco)
            .GreaterThan(0)
            .WithErrorCode(nameof(EntityValidationErrors.PrecoObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.PrecoObrigatorioParaProduto.Message);

        RuleFor(produto => produto.CategoriaId)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.CategoriaObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.CategoriaObrigatorioParaProduto.Message);

        RuleFor(produto => produto.FornecedorId)
            .NotEmpty()
            .WithErrorCode(nameof(EntityValidationErrors.FornecedorObrigatorioParaProduto))
            .WithMessage(EntityValidationErrors.FornecedorObrigatorioParaProduto.Message);
    }
}