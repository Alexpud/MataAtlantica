using AutoMapper;
using MataAtlantica.Domain.Models.Categorias;
using MataAtlantica.Domain.Profiles;
using MataAtlantica.Domain.Tests.Builder;

namespace MataAtlantica.Domain.Tests;

public class DomainProfileTests
{
    [Fact]
    public void DomainProfile_DeveSerValido()
    {
        // Arrange & Act & Assert
        new MapperConfiguration(p => p.AddProfile(typeof(DomainProfile)))
            .AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_DeveMapearCategoriaComSubCategoria()
    {
        // Arrange
        var categoriaPai = new CategoriaBuilder().BuildDefault().Create();
        var categoriaFilha = new CategoriaBuilder().BuildDefault().Create();
        categoriaPai.AdicionarSubCategoria(categoriaFilha);

        // Act
        var configuration = new MapperConfiguration(p => p.AddProfile(typeof(DomainProfile)));
        var dto = new Mapper(configuration).Map<CategoriaDto>(categoriaPai);

        // Assert
        Assert.Multiple(
            () => Assert.NotNull(dto),
            () => Assert.Equal(categoriaPai.Nome, dto.Nome),
            () => Assert.Single(dto.SubCategorias));
    }
}
