using AutoMapper;
using FluentResults;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Categorias.AdicionarSubCategoria;

public record AdicionarSubCategoriaCommand(string CategoriaId, string subCategoria) : IRequest<Result<CategoriaDto>> { }

internal class CommandHandler(
    ICategoriaRepository categoriaRepository, 
    IMapper mapper, 
    CategoriaService categoriaService) : IRequestHandler<AdicionarSubCategoriaCommand, Result<CategoriaDto>>
{
    private readonly CategoriaService _categoriaService = categoriaService;
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CategoriaDto>> Handle(AdicionarSubCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoriaResult = await _categoriaService.AdicionarSubCategoria(request.CategoriaId, request.subCategoria);
        if (categoriaResult.IsFailed)
            return Result.Fail(categoriaResult.Errors);
        _categoriaRepository.Adicionar(categoriaResult.Value);
        await _categoriaRepository.Commit();
        return Result.Ok(_mapper.Map<CategoriaDto>(categoriaResult.Value));
    }
}
