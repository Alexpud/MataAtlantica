using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Tests.Builder;
using NSubstitute;

namespace MataAtlantica.API.Tests.Domain.Services;

public class CategoriaServiceTests
{
    private readonly CategoriaService _sut;
    private readonly ICategoriaRepository _repository;
    public CategoriaServiceTests()
    {
        _repository = Substitute.For<ICategoriaRepository>();
        _sut = new(_repository);
    }

    [Fact]
    [Trait("Funcionalidade", "ListarCategoriasComoArvore")]
    public void ListarCategoriasComoArvore_DeveRetornarCategorias_ComOsPaisQuandoHouverem()
    {
        // Arrange
        var categoriaPai = new CategoriaBuilder().BuildDefault().ComNome("CategoriaPai").Create();
        var categoriaFilha = new CategoriaBuilder().BuildDefault().ComCategoriaPai(categoriaPai).Create();
        var categoriaFilha2 = new CategoriaBuilder().BuildDefault().ComCategoriaPai(categoriaPai).Create();

        // Act
        var categorias = _sut.ListarCategoriasComoArvore();

        // Assert
        Assert.Multiple(
            () => Assert.Single(categorias),
            () => Assert.Equal(2, categorias.FirstOrDefault().CategoriasFilhas.Count),
            () => Assert.Equal("CategoriaPai", categorias.FirstOrDefault().Nome));

    }
}
