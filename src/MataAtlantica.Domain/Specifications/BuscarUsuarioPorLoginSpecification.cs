using MataAtlantica.Domain.Entidades;

namespace MataAtlantica.Domain.Specifications;

public class BuscarUsuarioPorLoginSpecification : BaseSpecification<Usuario>
{
    public BuscarUsuarioPorLoginSpecification(string login)
    {
        Predicate = usuario => usuario.Login == login;
    }
}