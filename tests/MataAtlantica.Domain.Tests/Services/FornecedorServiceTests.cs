using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Services;
using MataAtlantica.Domain.Tests.Builder;
using NSubstitute;

namespace MataAtlantica.Domain.Tests.Services;

public class FornecedorServiceTests
{
    private readonly FornecedorService _sut;
    private readonly IValidator<AdicionarFornecedorDto> _criarFornecedorValidator;
    private readonly IValidator<AlterarFornecedorDto> _alterarFornecedorValidator;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IMapper _mapper;
    public FornecedorServiceTests()
    {
        _fornecedorRepository = Substitute.For<IFornecedorRepository>();
        _mapper = Substitute.For<IMapper>();
        _criarFornecedorValidator = Substitute.For<IValidator<AdicionarFornecedorDto>>();
        _alterarFornecedorValidator = Substitute.For<IValidator<AlterarFornecedorDto>>();
        _sut = new FornecedorService(_criarFornecedorValidator, _alterarFornecedorValidator, _fornecedorRepository, _mapper);
    }

    [Trait("Feature", "Criar Fornecedor")]
    [Fact(DisplayName = "Criacao de fornecedor deve falhar quando validacao dos dados falha")]
    public async Task Adicionar_DeveFalhar_QuandoDadosSãoInvalidos()
    {
        // Arrange
        _criarFornecedorValidator.ValidateAsync(Arg.Any<AdicionarFornecedorDto>())
            .Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure()
                }
            });

        var criarFornecedor = new CriarFornecedorBuilder().BuildDefault().Create();

        // Act
        var result = await _sut.Adicionar(criarFornecedor);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact(DisplayName = "Criacao de fornecedor deve criar um novo fornecedor quando bem sucedido")]
    [Trait("Feature", "Criar Fornecedor")]
    public async Task Adicionar_DeveInserirNovoFornecedor_QuandoBemSucedido()
    {
        // Arrange
        _criarFornecedorValidator
            .ValidateAsync(Arg.Any<AdicionarFornecedorDto>())
            .Returns(new ValidationResult());

        var criarFornecedor = new CriarFornecedorBuilder().BuildDefault().Create();

        _mapper.Map<FornecedorDto>(Arg.Any<Fornecedor>())
            .Returns(new FornecedorDto());

        // Act
        var result = await _sut.Adicionar(criarFornecedor);

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
    public async Task Alterar_DeveFalhar_QuandoValidacaoFalha()
    {
        // Arrange
        _alterarFornecedorValidator.ValidateAsync(Arg.Any<AlterarFornecedorDto>())
            .Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure()
                }
            });

        var alterarFornecedor = new AlterarFornecedorBuilder().BuildDefault().Create();

        // Act
        var result = await _sut.Alterar(alterarFornecedor);

        // Assert
        Assert.True(result.IsFailed);
    }


    [Trait("Feature", "Alterar Fornecedor")]
    [Fact(DisplayName = "Alterar fornecedor deve alterar fornecedor quando bem sucedido")]
    public async Task Alterar_DeveAlterar_QuandoBemSucedido()
    {
        // Arrange
        _alterarFornecedorValidator.ValidateAsync(Arg.Any<AlterarFornecedorDto>())
            .Returns(new ValidationResult());

        var fornecedorAAtualizar = new Fornecedor();
        _fornecedorRepository.ObterPorId(Arg.Any<string>())
            .Returns(fornecedorAAtualizar);

        var alterarFornecedor = new AlterarFornecedorBuilder().BuildDefault().Create();

        _mapper.Map<FornecedorDto>(Arg.Any<Fornecedor>())
            .Returns(new FornecedorDto());

        // Act
        var result = await _sut.Alterar(alterarFornecedor);

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