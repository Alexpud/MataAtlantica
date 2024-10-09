using AutoFixture;
using MataAtlantica.API.Domain.Entidades;

namespace MataAtlantica.API.Tests.Builder;

public abstract class BaseBuilder<TClass, TBuilder> 
    where TClass : class, new()
    where TBuilder : BaseBuilder<TClass, TBuilder>
{
    protected TClass Object;

    public virtual TBuilder BuildDefault()
    {
        Object = new();
        return (TBuilder)this;
    }

    public virtual TClass Create()
        => Object;
}

//public class CategoriaBuilder : BaseBuilder<Categoria, CategoriaBuilder>
//{
//    private readonly Fixture _fixture = new();
//    public override CategoriaBuilder BuildDefault()
//    {
//        Object = new()
//        {
//            Id = _fixture.Create<string>(),
//            Nome = _fixture.Create<string>()
//        };
//        return this;
//    }

//    public CategoriaBuilder ComCategoriaPai(Categoria categoria)
//    {
//        Object.SetCategoriaPai(categoria);
//        return this;
//    }

//    public CategoriaBuilder ComNome(string nome)
//    {
//        Object.Nome = nome;
//        return this;
//    }
//}
