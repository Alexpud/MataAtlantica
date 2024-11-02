using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Helpers;

namespace MataAtlantica.API.Domain.Services;

public class FornecedorService(
    IValidator<CriarFornecedor> criarFornecedorValidator,
    IFornecedorRepository fornecedorRepository,
    IMapper mapper)
{
    private readonly IValidator<CriarFornecedor> _criarFornecedorValidator = criarFornecedorValidator;
    private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<FornecedorDto>> CriarFornecedor(CriarFornecedor criarFornecedorDto)
    {
        var validationResult = await _criarFornecedorValidator.ValidateAsync(criarFornecedorDto);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.GetErrors());

        var fornecedor = new Fornecedor(criarFornecedorDto);
        _fornecedorRepository.Adicionar(fornecedor);
        await _fornecedorRepository.Commit();

        var dto = _mapper.Map<FornecedorDto>(fornecedor);
        return Result.Ok(dto);
    }

    public async Task<FornecedorDto> ObterPorId(string id)
    {
        var fornecedor = await _fornecedorRepository.ObterPorId(id);
        return fornecedor == null ? null : _mapper.Map<FornecedorDto>(fornecedor);
    }
}