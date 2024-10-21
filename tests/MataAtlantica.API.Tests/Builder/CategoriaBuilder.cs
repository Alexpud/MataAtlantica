using AutoFixture;
using MataAtlantica.API.Domain.Entidades;

namespace MataAtlantica.API.Tests.Builder;

public class CategoriaBuilder : BaseBuilder<Categoria, CategoriaBuilder>
{
    private readonly Fixture _fixture = new();
    private string _nome;
    private List<Categoria> _subCategorias = new();
    private Categoria _categoriaPai;
    public override CategoriaBuilder BuildDefault()
    {
        _nome = _fixture.Create<string>();
        return this;
    }

    public CategoriaBuilder ComCategoriaPai(Categoria categoria)
    {
        _categoriaPai = categoria;
        return this;
    }

    public CategoriaBuilder ComNome(string nome)
    {
        Object.Nome = nome;
        return this;
    }

    public override Categoria Create()
    {
        Object = new Categoria(_nome);
        if (_categoriaPai != null)
            Object.SetCategoriaPai(_categoriaPai);
        return Object;
    }
}
