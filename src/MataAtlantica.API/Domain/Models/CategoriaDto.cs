namespace MataAtlantica.API.Domain.Models;

public record CategoriaDto(string Id, string Nome, string CategoriaPaiId, List<CategoriaDto> SubCategorias);
