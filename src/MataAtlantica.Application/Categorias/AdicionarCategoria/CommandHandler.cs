using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Categorias;
using MediatR;

namespace MataAtlantica.Application.Categorias.AdicionarCategoria;

public class CommandHandler(ICategoriaRepository categoriaRepository,
    IMapper mapper) : IRequestHandler<AdicionarCategoriaCommand, CommandResponse<CategoriaDto>>
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CommandResponse<CategoriaDto>> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = new Categoria(request.Nome);
        _categoriaRepository.Adicionar(categoria);
        await _categoriaRepository.Commit();

        return new CommandResponse<CategoriaDto>(_mapper.Map<CategoriaDto>(categoria));
    }
}
