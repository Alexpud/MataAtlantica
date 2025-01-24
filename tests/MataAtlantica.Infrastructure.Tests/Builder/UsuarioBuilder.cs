using AutoFixture;
using MataAtlantica.Domain.Entidades;

namespace MataAtlantica.Infrastructure.Tests.Builder;

internal class UsuarioBuilder : BaseBuilder<Usuario, UsuarioBuilder>
{
    private readonly Fixture _fixture = new Fixture();

    public override UsuarioBuilder BuildDefault()
    {
        Object = new Usuario(
            id: _fixture.Create<string>(),
            nome: _fixture.Create<string>(),
            sobrenome: _fixture.Create<string>(),
            login: _fixture.Create<string>(),
            endereco: null);
        return this;
    }
}
