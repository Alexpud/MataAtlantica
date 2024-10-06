using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Repositories.Abstract;

namespace MataAtlantica.API.Infrastructure;

public class CategoriaRepository : ICategoriaRepository
{
    public IQueryable<Categoria> AsQueryable()
    {
        throw new NotImplementedException();
    }
}
