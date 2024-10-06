using MataAtlantica.API.Domain.Entidades;

namespace MataAtlantica.API.Domain.Repositories.Abstract;

public interface ICategoriaRepository
{
    IQueryable<Categoria> AsQueryable();
}
