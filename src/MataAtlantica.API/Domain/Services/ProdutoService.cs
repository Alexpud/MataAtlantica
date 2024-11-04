using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Helpers;

namespace MataAtlantica.API.Domain.Services;

public class ProdutoService(
    IValidator<CriarProduto> criarProdutoValidator,
    IProdutoRepository produtoRepository, 
    IMapper mapper)
{
    private readonly IValidator<CriarProduto> _criarProdutoValidator = criarProdutoValidator;
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ProdutoDto> ObterPorId(string id)
    {
        var produto = await _produtoRepository.ObterPorId(id);
        return produto == null ? null : _mapper.Map<ProdutoDto>(produto);
    }

    public async Task<Result<ProdutoDto>> CriarProduto(CriarProduto model)
    {
        var validation = await _criarProdutoValidator.ValidateAsync(model);
        if (!validation.IsValid)
            return Result.Fail(validation.GetErrors());

        var produto = new Produto(model);
        _produtoRepository.Adicionar(produto);
        await _produtoRepository.Commit();

        return Result.Ok(_mapper.Map<ProdutoDto>(produto));
    }
}
