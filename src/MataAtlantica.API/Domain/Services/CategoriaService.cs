using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;

namespace MataAtlantica.API.Domain.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository)
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public List<CategoriaDto> ListarCategoriasComoArvore()
    {
        //var categorias = _categoriaRepository.Filtrar();
        //categorias.Select(p => )
        throw new NotImplementedException();
    }
}
