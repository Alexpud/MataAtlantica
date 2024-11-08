namespace MataAtlantica.API.Domain.Models;

public record AdicionarProdutoDto(string Nome, string CategoriaId, float Preco, string Descricao, string FornecedorId, string Marca);
