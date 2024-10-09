namespace MataAtlantica.API.Domain.Models;

public record CategoriaDto(string Nome, CategoriaDto CategoriaPai, List<CategoriaDto> SubCategorias);
