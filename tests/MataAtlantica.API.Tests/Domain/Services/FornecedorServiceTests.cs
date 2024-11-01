using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.API.Domain.Erros;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Domain.Services;
using NSubstitute;

namespace MataAtlantica.API.Tests.Domain.Services;

public class FornecedorServiceTests
{
    private readonly FornecedorService _sut;
    private readonly IValidator<CriarFornecedor> _criarFornecedorValidator;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IMapper _mapper;
    public FornecedorServiceTests()
    {
        _fornecedorRepository = Substitute.For<IFornecedorRepository>();
        _mapper = Substitute.For<IMapper>();
        _criarFornecedorValidator = Substitute.For<IValidator<CriarFornecedor>>();
        _sut = new FornecedorService(_criarFornecedorValidator, _fornecedorRepository, _mapper);
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
        var result = await _sut.CriarFornecedor(criarFornecedor);

        // Assert
        Assert.True(result.IsFailed);
    }
}

public abstract class BaseBuilder<TEntity, TBuilder> where TEntity : class
{
    protected TEntity Object;

    public abstract TBuilder BuildDefault();

    public abstract TEntity Create();
}

public class CriarFornecedorBuilder : BaseBuilder<CriarFornecedor, CriarFornecedorBuilder>
{
    public override CriarFornecedorBuilder BuildDefault()
    {
        Object = new CriarFornecedor(
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
                CEP: string.Empty));
        return this;
    }

    public override CriarFornecedor Create()
    {
        throw new NotImplementedException();
    }
}
