using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Helpers;

namespace MataAtlantica.API.Domain.Services;

public class ProdutoService(
    IValidator<AdicionarProdutoDto> criarProdutoValidator,
    IProdutoRepository produtoRepository, 
    IMapper mapper)
{
    private readonly IValidator<AdicionarProdutoDto> _criarProdutoValidator = criarProdutoValidator;
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

    public List<ProdutoDto> BuscarProdutos(BuscarProdutosArgs args)
    {
        var produtos = _produtoRepository.Buscar(args);
        return produtos.ToList();
    }

    /// <summary>
    /// Adiciona thumbnail a produto, se houver thumbnail com a mesma ordem, nao adiciona.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task AdicionarThumbnail(AdicionarThumbnailProdutoDto dto)
    {
        var produto = await _produtoRepository.ObterPorId(dto.ProdutoId);
        produto.AdicionarImagemThumbnail(dto.Ordem);
        await _produtoRepository.Commit();
    }
}
