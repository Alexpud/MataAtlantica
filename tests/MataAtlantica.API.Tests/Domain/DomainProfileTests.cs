﻿using AutoMapper;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Profiles;
using MataAtlantica.API.Tests.Builder;

namespace MataAtlantica.API.Tests.Domain;

public class DomainProfileTests
{
    [Fact]
    public void DomainProfile_DeveSerValido()
    {
        var configuration = new MapperConfiguration(p => p.AddProfile(typeof(DomainProfile)));

        configuration.AssertConfigurationIsValid();
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
            () => Assert.Equal(1, dto.SubCategorias.Count));
    }
}
