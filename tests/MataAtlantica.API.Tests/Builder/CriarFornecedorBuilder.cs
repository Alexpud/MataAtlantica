using AutoFixture;
using MataAtlantica.Domain.Models;

namespace MataAtlantica.API.Tests.Builder;

public class CriarFornecedorBuilder : BaseBuilder<AdicionarFornecedorDto, CriarFornecedorBuilder>
{
    private string _cpfCnpj = string.Empty;

    public override AdicionarFornecedorDto Create()
    {
        Object = new AdicionarFornecedorDto(
            Nome: string.Empty,
            Descricao: string.Empty,
            CpfCnpj: _cpfCnpj,
            Telefone: string.Empty,
            Localizacao: new EnderecoFornecedor(
                Rua: string.Empty,
                Bairro: string.Empty,
                Numero: string.Empty,
                Cidade: string.Empty,
                UF: string.Empty,
                CEP: string.Empty));

        return Object;
    }

    public CriarFornecedorBuilder ComCpfCnpj(string cpfCnpj)
    {
        _cpfCnpj = cpfCnpj;
        return this;
    }
}

public class AlterarFornecedorBuilder : BaseBuilder<AlterarFornecedorDto, AlterarFornecedorBuilder>
{
    private readonly Fixture _fixture = new Fixture();
    private string _cpfCnpj = string.Empty;

    public override AlterarFornecedorDto Create()
    {
        Object = new AlterarFornecedorDto(
            Id: _fixture.Create<string>(),
            Nome: _fixture.Create<string>(),
            Descricao: _fixture.Create<string>(),
            CpfCnpj: _cpfCnpj,
            Telefone: _fixture.Create<string>(),
            Localizacao: new EnderecoFornecedor(
                Rua: _fixture.Create<string>(),
                Bairro: _fixture.Create<string>(),
                Numero: _fixture.Create<string>(),
                Cidade: _fixture.Create<string>(),
                UF: _fixture.Create<string>(),
                CEP: _fixture.Create<string>()));

        return Object;
    }

    public AlterarFornecedorBuilder ComCpfCnpj(string cpfCnpj)
    {
        _cpfCnpj = cpfCnpj;
        return this;
    }
}
