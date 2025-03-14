using AutoMapper;
using FluentResults;
using MataAtlantica.Application.Common;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Models.Categorias;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Categorias.AdicionarSubCategoria;

public record AdicionarSubCategoriaCommand(string CategoriaId, string subCategoria) : IRequest<CommandResponse<CategoriaDto>> { }

internal class CommandHandler(
    ICategoriaRepository categoriaRepository,
    IMapper mapper,
    CategoriaService categoriaService) : IRequestHandler<AdicionarSubCategoriaCommand, CommandResponse<CategoriaDto>>
{
    private readonly CategoriaService _categoriaService = categoriaService;
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CommandResponse<CategoriaDto>> Handle(AdicionarSubCategoriaCommand request, CancellationToken cancellationToken)
    {
        var response = new CommandResponse<CategoriaDto>();
        var categoriaResult = await _categoriaService.AdicionarSubCategoria(request.CategoriaId, request.subCategoria);
        if (categoriaResult.IsFailed)
        {
            response.WithErrors(categoriaResult.Errors);
            return response;
        }
        _categoriaRepository.Adicionar(categoriaResult.Value);
        await _categoriaRepository.Commit();
        response.SetValue(_mapper.Map<CategoriaDto>(categoriaResult.Value));
        return response;
    }
}
