using FluentValidation.TestHelper;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Validators;
using MataAtlantica.Domain.Tests.Builder;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MataAtlantica.Domain.Tests.Models;
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
    [Fact(DisplayName = "TestValidate deve falhar quando ja existir fornecedor com o mesmo Cpf/Cnpj")]
    public async Task TestValidate_DeveFalhar_QuandoCpfCnpjJaExistir()
    {
        // Arrange
        _fornecedorRepository.ObterPorCpfCnpj(Arg.Any<string>())
            .Returns(new Fornecedor());


        var criarFornecedor = new CriarFornecedorBuilder().BuildDefault().ComCpfCnpj("ALGO").Create();

        // Act
        var result = await _sut.TestValidateAsync(criarFornecedor);

        // Assert
        var error = result.Errors.First(p => p.PropertyName == nameof(criarFornecedor.CpfCnpj));
        Assert.Multiple(
            () => result.ShouldHaveValidationErrorFor(p => p.CpfCnpj),
            () => Assert.Equal(nameof(BusinessErrors.FornecedorComCpfCnpjJaExiste), error?.ErrorCode),
            () => Assert.Equal(BusinessErrors.FornecedorComCpfCnpjJaExiste.Message, error?.ErrorMessage));
    }

    [Trait("Feature", "Criar Fornecedor")]
    [Fact(DisplayName = "TestValidate nao deve falhar quando Cpf/Cnpj for valido")]
    public async Task TestValidate_NaoDeveTerErroDeValidacaoParaCpfCnpj_QuandoCpfCnpjForValido()
    {
        // Arrange
        _fornecedorRepository.ObterPorCpfCnpj(Arg.Any<string>())
            .ReturnsNull();


        var criarFornecedor = new CriarFornecedorBuilder().BuildDefault().ComCpfCnpj("lol").Create();

        // Act
        var result = await _sut.TestValidateAsync(criarFornecedor);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.CpfCnpj);
    }
}
