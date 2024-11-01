using AutoMapper;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;

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
            return Result.Fail(GetErrors(validationResult));
        throw new NotImplementedException();
    }

    private IEnumerable<IError> GetErrors(ValidationResult validationResult)
    {
        foreach(var error in validationResult.Errors)
            yield return new Error(error.ErrorMessage).WithMetadata("ErrorCode", error.ErrorCode);
    }

    public async Task<FornecedorDto> ObterPorId(string id)
    {
        var fornecedor = await _fornecedorRepository.ObterPorId(id);
        return fornecedor == null ? null : _mapper.Map<FornecedorDto>(fornecedor);
    }
}