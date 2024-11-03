using FluentValidation.TestHelper;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Erros;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Models.Validators;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Tests.Builder;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MataAtlantica.API.Tests.Domain.Models;

public class AlterarFornecedorValidatorTests
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly AlterarFornecedorValidator _sut;

    public AlterarFornecedorValidatorTests()
    {
        _fornecedorRepository = Substitute.For<IFornecedorRepository>();
        _sut = new AlterarFornecedorValidator(_fornecedorRepository);
    }

    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName = "TestValidate deve falhar quando fornecedor nao for encontrado")]
    public async Task TestValidate_DeveFalhar_QuandoNaoExistirFornecedor()
    {
        // Arrange
        var alterarFornecedor = new AlterarFornecedorBuilder().BuildDefault().Create();

        _fornecedorRepository.ObterPorId(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var result = await _sut.TestValidateAsync(alterarFornecedor);

        // Assert
        var error = result.Errors.FirstOrDefault(p => p.PropertyName ==  nameof(AlterarFornecedor.Id));
        Assert.Multiple(
            () => result.ShouldHaveValidationErrorFor(p => p.Id),
            () => Assert.Equal(BusinessErrors.FornecedorNaoEncontrado.Message, error?.ErrorMessage),
            () => Assert.Equal(nameof(BusinessErrors.FornecedorNaoEncontrado), error?.ErrorCode));
    }

    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName = "TestValidate nao deve ter erros para Id quando propriedade for valida")]
    public async Task TestValidate_NaoDeveTerErroDeValidacaoParaId_QuandoIdForValido()
    {
        // Arrange
        var alterarFornecedor = new AlterarFornecedorBuilder().BuildDefault().Create();

        _fornecedorRepository.ObterPorId(Arg.Any<string>())
            .Returns(new Fornecedor());

        // Act
        var result = await _sut.TestValidateAsync(alterarFornecedor);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.Id);
    }

    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName ="TestValidate deve falhar quando ja existir fornecedor com o mesmo Cpf/Cnpj e referenciar fornecedor diferente")]
    public async Task TestValidate_DeveFalhar_QuandoCpfCnpjJaExistir_E_ReferenciarFornecedorDiferenete()
    {
        // Arrange
        _fornecedorRepository
            .ObterPorCpfCnpj(Arg.Any<string>())
            .Returns(new Fornecedor());

        var criarFornecedor = new AlterarFornecedorBuilder().BuildDefault().ComCpfCnpj("algo").Create();

        // Act
        var result = await _sut.TestValidateAsync(criarFornecedor);

        // Assert
        var error = result.Errors.FirstOrDefault(p => p.PropertyName == nameof(AlterarFornecedor.CpfCnpj));
        Assert.Multiple(
            () => result.ShouldHaveValidationErrorFor(p => p.CpfCnpj),
            () => Assert.Equal(BusinessErrors.FornecedorComCpfCnpjJaExiste.Message, error?.ErrorMessage),
            () => Assert.Equal(nameof(BusinessErrors.FornecedorComCpfCnpjJaExiste), error?.ErrorCode));
    }

    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName = "TestValidate nao deve ter erros para propriedade cpfCnpj quando for valida")]
    public async Task TestValidate_NaoDeveTerErroDeValidacaoParaCpfCnpj_QuandoCpfCnpjForValido()
    {
        // Arrange
        _fornecedorRepository.ObterPorCpfCnpj(Arg.Any<string>())
            .ReturnsNull();

        var criarFornecedor = new AlterarFornecedorBuilder().BuildDefault().ComCpfCnpj("algo").Create();

        // Act
        var result = await _sut.TestValidateAsync(criarFornecedor);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.CpfCnpj);
    }
}
