namespace MataAtlantica.API.Domain.Models;

public record CriarProduto(string Nome, string CategoriaId, float Preco, string Descricao, string FornecedorId, string Marca);
