using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Helpers;
using MataAtlantica.Domain.Models.Fornecedores;

namespace MataAtlantica.Domain.Services;

public class FornecedorService(
    IValidator<AdicionarFornecedorDto> criarFornecedorValidator,
    IValidator<AlterarFornecedorDto> alterarFornecedorValidator,
    IFornecedorRepository fornecedorRepository,
    IMapper mapper)
{
    private readonly IValidator<AdicionarFornecedorDto> _criarFornecedorValidator = criarFornecedorValidator;
    private readonly IValidator<AlterarFornecedorDto> _alterarFornecedorValidator = alterarFornecedorValidator;
    private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<FornecedorDto>> Alterar(AlterarFornecedorDto alterarFornecedor)
    {
        var validationResult = await _alterarFornecedorValidator.ValidateAsync(alterarFornecedor);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.GetErrors());

        var fornecedor = await _fornecedorRepository.ObterPorId(alterarFornecedor.Id);
        fornecedor.AtualizarAPartirDe(alterarFornecedor);

        await _fornecedorRepository.Commit();

        return Result.Ok(_mapper.Map<FornecedorDto>(fornecedor));
    }

    public async Task<Result<FornecedorDto>> Adicionar(AdicionarFornecedorDto criarFornecedorDto)
    {
        var validationResult = await _criarFornecedorValidator.ValidateAsync(criarFornecedorDto);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.GetErrors());

        var fornecedor = new Fornecedor(criarFornecedorDto);
        _fornecedorRepository.Adicionar(fornecedor);
        await _fornecedorRepository.Commit();

        return Result.Ok(_mapper.Map<FornecedorDto>(fornecedor));
    }
}