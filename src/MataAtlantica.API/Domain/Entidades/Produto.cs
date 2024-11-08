using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Entidades;

public class Produto : EntidadeBase
{
    public Produto() { }
    public Produto(AdicionarProdutoDto model)
    {
        Nome = model.Nome;
        Descricao = model.Descricao;
        Marca = model.Marca;
        Preco = model.Preco;
        CategoriaId = model.CategoriaId;
        FornecedorId = model.FornecedorId;
    }

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
