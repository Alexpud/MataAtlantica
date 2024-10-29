using AutoMapper;
using FluentResults;
using MataAtlantica.API.Domain.Erros;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;

namespace MataAtlantica.API.Domain.Services;

public class FornecedorService(
    IFornecedorRepository fornecedorRepository,
    IMapper mapper)
{
    private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result<FornecedorDto>> ObterPorId(string id)
    {
        var fornecedor = await _fornecedorRepository.ObterPorId(id);
        if (fornecedor == null)
            return Result.Fail(BusinessErrors.FornecedorNaoEncontrado);
        
        return _mapper.Map<FornecedorDto>(fornecedor);
    }
}