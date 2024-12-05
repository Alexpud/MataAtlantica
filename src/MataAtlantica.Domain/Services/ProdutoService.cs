using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Helpers;
using MataAtlantica.Domain.Models;

namespace MataAtlantica.Domain.Services;

public class ProdutoService(
    IValidator<AdicionarProdutoDto> criarProdutoValidator,
    IValidator<AlterarProdutoDto> atualizarProdutoValidator,
    IValidator<AdicionarImagemProdutoDto> adicionarImagemProdutoValidator,
    IProdutoRepository produtoRepository,
    IMapper mapper)
{
    private readonly IValidator<AdicionarProdutoDto> _criarProdutoValidator = criarProdutoValidator;
    private readonly IValidator<AlterarProdutoDto> _atualizarProdutoValidator = atualizarProdutoValidator;
    private readonly IValidator<AdicionarImagemProdutoDto> _adicionarImagemProdutoValidator = adicionarImagemProdutoValidator;
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ProdutoDto> ObterPorId(string id)
    {
        var produto = await _produtoRepository.ObterPorId(id);
        return produto == null ? null : _mapper.Map<ProdutoDto>(produto);
    }

    public async Task<Result<ProdutoDto>> Adicionar(AdicionarProdutoDto model)
    {
        var validation = await _criarProdutoValidator.ValidateAsync(model);
        if (!validation.IsValid)
            return Result.Fail(validation.GetErrors());

        var produto = new Produto(model);
        _produtoRepository.Adicionar(produto);
        await _produtoRepository.Commit();

        return Result.Ok(_mapper.Map<ProdutoDto>(produto));
    }

    public async Task<Result<ProdutoDto>> Alterar(AlterarProdutoDto model)
    {
        var validation = await _atualizarProdutoValidator.ValidateAsync(model);
        if (!validation.IsValid)
            return Result.Fail(validation.GetErrors());

        var produto = await _produtoRepository.ObterPorId(model.ProdutoId);
        produto.AtualizarDe(model);
        await _produtoRepository.Commit();

        return Result.Ok(_mapper.Map<ProdutoDto>(produto));
    }

    public List<ProdutoDto> BuscarProdutos(BuscarProdutosArgs args)
    {
        var produtos = _produtoRepository.Buscar(args);
        return produtos.ToList();
    }

    /// <summary>
    /// Adiciona thumbnail a produto, se houver thumbnail com a mesma ordem, nao adiciona.
    /// </summary>
    /// <param name="produtoId">Id do produto associado</param>
    /// <param name="ordem">Ordem de exibicao da imagem</param>
    /// <returns></returns>
    public async Task<Result> AdicionarThumbnail(AdicionarImagemProdutoDto dto)
    {
        var validation = await _adicionarImagemProdutoValidator.ValidateAsync(dto);
        if (!validation.IsValid)
            return Result.Fail(validation.GetErrors());

        var produto = await _produtoRepository.ObterPorId(dto.ProdutoId);
        produto.AdicionarImagemThumbnail(dto.Ordem);
        await _produtoRepository.Commit();
        return Result.Ok();
    }

    public async Task AdicionarImagemIlustrativa(AdicionarImagemProdutoDto dto)
    {
        var produto = await _produtoRepository.ObterPorId(dto.ProdutoId);
        produto.AdicionarImagemIlustrativa(dto.Ordem);
        await _produtoRepository.Commit();
    }
}
