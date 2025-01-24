namespace MataAtlantica.Domain.Models.Categorias;

public record CategoriaDto(string Id, string Nome, string CategoriaPaiId, List<CategoriaDto> SubCategorias);
