using AutoMapper;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Models.Fornecedores;
using MediatR;

namespace MataAtlantica.Application.Fornecedores.Listar;

public record ListarFornecedoresQuery : IRequest<IEnumerable<FornecedorDto>> { }

public class QueryHandler(IMapper mapper, IFornecedorRepository fornecedorRepository) : IRequestHandler<ListarFornecedoresQuery, IEnumerable<FornecedorDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
    public Task<IEnumerable<FornecedorDto>> Handle(ListarFornecedoresQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_mapper.ProjectTo<FornecedorDto>(_fornecedorRepository.AsQueryable()).AsEnumerable());
    }
}

