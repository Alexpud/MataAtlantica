namespace MataAtlantica.API.Domain.Models;

public record CategoriaDto(string Nome, List<CategoriaDto> CategoriasFilhas);
