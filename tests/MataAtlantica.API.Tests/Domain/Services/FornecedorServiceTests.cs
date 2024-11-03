using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Erros;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Tests.Builder;
using NSubstitute;

namespace MataAtlantica.API.Tests.Domain.Services;

public class FornecedorServiceTests
{
    private readonly FornecedorService _sut;
    private readonly IValidator<CriarFornecedor> _criarFornecedorValidator;
    private readonly IValidator<AlterarFornecedor> _alterarFornecedorValidator;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IMapper _mapper;
    public FornecedorServiceTests()
    {
        _fornecedorRepository = Substitute.For<IFornecedorRepository>();
        _mapper = Substitute.For<IMapper>();
        _criarFornecedorValidator = Substitute.For<IValidator<CriarFornecedor>>();
        _alterarFornecedorValidator = Substitute.For<IValidator<AlterarFornecedor>>();
        _sut = new FornecedorService(_criarFornecedorValidator, _alterarFornecedorValidator, _fornecedorRepository, _mapper);
    }

    [Trait("Feature", "Criar Fornecedor")]
    [Fact(DisplayName = "Criacao de fornecedor deve falhar quando validacao dos dados falha")]
    public async Task CriarFornecedor_DeveFalhar_QuandoDadosSãoInvalidos()
    {
        // Arrange
        _criarFornecedorValidator.ValidateAsync(Arg.Any<CriarFornecedor>())
            .Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure()
                }
            });

        var criarFornecedor = new CriarFornecedorBuilder().BuildDefault().Create();

        // Act
        var result = await _sut.CriarFornecedor(criarFornecedor);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact(DisplayName = "Criacao de fornecedor deve criar um novo fornecedor quando bem sucedido")]
    [Trait("Feature", "Criar Fornecedor")]
    public async Task CriarFornecedor_DeveInserirNovoFornecedor_QuandoBemSucedido()
    {
        // Arrange
        _criarFornecedorValidator
            .ValidateAsync(Arg.Any<CriarFornecedor>())
            .Returns(new ValidationResult());

        var criarFornecedor = new CriarFornecedorBuilder().BuildDefault().Create();

        _mapper.Map<FornecedorDto>(Arg.Any<Fornecedor>())
            .Returns(new FornecedorDto());

        // Act
        var result = await _sut.CriarFornecedor(criarFornecedor);

        // Assert
        Assert.Multiple(
            () => Assert.True(result.IsSuccess),
            () => _fornecedorRepository.Received(1).Commit(),
            () => _fornecedorRepository.Adicionar(Arg.Is<Fornecedor>(p => 
                p.Nome == criarFornecedor.Nome 
                && p.CpfCnpj == criarFornecedor.CpfCnpj
                && p.Descricao == criarFornecedor.Descricao
                && p.Localizacao.Rua == criarFornecedor.Localizacao.Rua
                && p.Localizacao.Bairro == criarFornecedor.Localizacao.Bairro
                && p.Localizacao.CEP == criarFornecedor.Localizacao.CEP
                && p.Localizacao.UF == criarFornecedor.Localizacao.UF
                && p.Localizacao.Cidade == criarFornecedor.Localizacao.Cidade)));
    }

    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName = "Alterar fornecedor deve falhar quando validacao dos dados falha")]
    public async Task AlterarFornecedor_DeveFalhar_QuandoValidacaoFalha()
    {
        // Arrange
        _alterarFornecedorValidator.ValidateAsync(Arg.Any<AlterarFornecedor>())
            .Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure()
                }
            });

        var alterarFornecedor = new AlterarFornecedorBuilder().BuildDefault().Create();

        // Act
        var result = await _sut.AlterarFornecedor(alterarFornecedor);

        // Assert
        Assert.True(result.IsFailed);
    }


    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName = "Alterar fornecedor deve alterar fornecedor quando bem sucedido")]
    public async Task AlterarFornecedor_DeveAlterarFornecedor_QuandoBemSucedido()
    {
        // Arrange
        _alterarFornecedorValidator.ValidateAsync(Arg.Any<AlterarFornecedor>())
            .Returns(new ValidationResult());

        var fornecedorAAtualizar = new Fornecedor();
        _fornecedorRepository.ObterPorId(Arg.Any<string>())
            .Returns(fornecedorAAtualizar);

        var alterarFornecedor = new AlterarFornecedorBuilder().BuildDefault().Create();

        _mapper.Map<FornecedorDto>(Arg.Any<Fornecedor>())
            .Returns(new FornecedorDto());

        // Act
        var result = await _sut.AlterarFornecedor(alterarFornecedor);

        // Assert
        Assert.Multiple(
            () => Assert.True(result.IsSuccess),
            () => _fornecedorRepository.Received(1).Commit(),
            () => Assert.Equal(alterarFornecedor.Nome, fornecedorAAtualizar.Nome),
            () => Assert.Equal(alterarFornecedor.Telefone, fornecedorAAtualizar.Telefone),
            () => Assert.Equal(alterarFornecedor.CpfCnpj, fornecedorAAtualizar.CpfCnpj),
            () => Assert.Equal(alterarFornecedor.Descricao, fornecedorAAtualizar.Descricao),
            () => Assert.Equal(alterarFornecedor.Localizacao.Cidade, fornecedorAAtualizar.Localizacao.Cidade),
            () => Assert.Equal(alterarFornecedor.Localizacao.Bairro, fornecedorAAtualizar.Localizacao.Bairro),
            () => Assert.Equal(alterarFornecedor.Localizacao.Rua, fornecedorAAtualizar.Localizacao.Rua),
            () => Assert.Equal(alterarFornecedor.Localizacao.Numero, fornecedorAAtualizar.Localizacao.Numero),
            () => Assert.Equal(alterarFornecedor.Localizacao.CEP, fornecedorAAtualizar.Localizacao.CEP),
            () => Assert.Equal(alterarFornecedor.Localizacao.UF, fornecedorAAtualizar.Localizacao.UF));
    }
}