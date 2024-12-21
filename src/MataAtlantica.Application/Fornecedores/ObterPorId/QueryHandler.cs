using AutoMapper;
using FluentResults;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.ObterPorId;

public record ObterFornecedorPorIdQuery(string Id) : IRequest<Result<FornecedorDto>> { }

public class QueryHandler(IFornecedorRepository fornecedorRepository, IMapper mapper) : IRequestHandler<ObterFornecedorPorIdQuery, Result<FornecedorDto>>
{
    private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<FornecedorDto>> Handle(ObterFornecedorPorIdQuery request, CancellationToken cancellationToken)
    {
        var fornecedor = await _fornecedorRepository.ObterPorId(request.Id);
        return fornecedor == null 
            ? Result.Fail(BusinessErrors.FornecedorNaoEncontrado) 
            : _mapper.Map<FornecedorDto>(fornecedor);
    }
}
