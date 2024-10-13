namespace MataAtlantica.API.Domain.Models;

public record CategoriaDto(string Id, string Nome, List<CategoriaDto> SubCategorias);
