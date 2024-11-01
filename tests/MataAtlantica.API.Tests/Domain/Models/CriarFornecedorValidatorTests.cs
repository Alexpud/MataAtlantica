using FluentValidation.TestHelper;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MataAtlantica.API.Tests.Domain.Models;
public class CriarFornecedorValidatorTests
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly CriarFornecedorValidator _sut;

    public CriarFornecedorValidatorTests()
    {
        _fornecedorRepository = Substitute.For<IFornecedorRepository>();
        _sut = new CriarFornecedorValidator(_fornecedorRepository);
    }

    [Trait("Feature", "Criar Fornecedor")]
    [Fact(DisplayName ="TestValidate deve falhar quando ja existir fornecedor com o mesmo Cpf/Cnpj")]
    public async Task TestValidate_DeveFalhar_QuandoCpfCnpjJaExistir()
    {
        // Arrange
        _fornecedorRepository.ObterPorCpfCnpj(Arg.Any<string>())
            .Returns(new Fornecedor());


        var criarFornecedor = new CriarFornecedor(
            Nome: string.Empty,
            Descricao: string.Empty,
            CpfCnpj: string.Empty,
            Telefone: string.Empty,
            Localizacao: new EnderecoFornecedor(
                Rua: string.Empty,
                Bairro: string.Empty,
                Numero: string.Empty,
                Cidade: string.Empty,
                UF: string.Empty,
                CEP: string.Empty
            ));

        // Act
        var result = await _sut.TestValidateAsync(criarFornecedor);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.CpfCnpj);
    }

    [Trait("Feature", "Criar Fornecedor")]
    [Fact(DisplayName = "TestValidate nao deve falhar quando Cpf/Cnpj for valido")]
    public async Task TestValidate_NaoDeveTerErroDeValidacaoParaCpfCnpj_QuandoCpfCnpjForValido()
    {
        // Arrange
        _fornecedorRepository.ObterPorCpfCnpj(Arg.Any<string>())
            .ReturnsNull();


        var criarFornecedor = new CriarFornecedor(
            Nome: string.Empty,
            Descricao: string.Empty,
            CpfCnpj: "algum cpf/cnpj",
            Telefone: string.Empty,
            Localizacao: new EnderecoFornecedor(
                Rua: string.Empty,
                Bairro: string.Empty,
                Numero: string.Empty,
                Cidade: string.Empty,
                UF: string.Empty,
                CEP: string.Empty
            ));

        // Act
        var result = await _sut.TestValidateAsync(criarFornecedor);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.CpfCnpj);
    }
}
