using MataAtlantica.API.Domain.Entidades;

namespace MataAtlantica.API.Domain.Models;

public class ProdutoDto
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public float Preco { get; set; }
    public ConfiguracaoImagens ConfiguracaoImagens { get; set; }
    public string Categoria { get; set; }
    public string NomeFornecedor { get; set; }
}
