using AutoMapper;
using MataAtlantica.API.Domain.Profiles;

namespace MataAtlantica.API.Tests.Domain.Services;

public class CategoriaServiceTests
{
    [Fact]
    public void DomainProfile_DeveSerValido()
    {
        var configuration = new MapperConfiguration(p => p.AddProfile(typeof(DomainProfile)));

        configuration.AssertConfigurationIsValid();
    }
}
