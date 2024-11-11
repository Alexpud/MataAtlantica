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

    public void AdicionarImagemThumbnail(int ordem)
    {
        ConfiguracaoImagens ??= new ConfiguracaoImagens()
            {
                Thumbnails = new List<Imagem>()
            };
        var thumbnailExistente = ConfiguracaoImagens.Thumbnails.FirstOrDefault(p => p.Ordem == ordem);
        if (thumbnailExistente != null)
            thumbnailExistente = new Imagem(ordem);
        else
            ConfiguracaoImagens.Thumbnails.Add(new Imagem(ordem));
    }

    public void AdicionarImagemIlustrativa(int ordem)
        =>ConfiguracaoImagens.ImagensIlustrativas.Add(new Imagem(ordem));
}

public class ConfiguracaoImagens
{
    public List<Imagem> Thumbnails { get; set; }
    public List<Imagem> ImagensIlustrativas { get; set; }
}

public class Imagem(int Ordem)
{
    public int Ordem { get; set; } = Ordem;
}
