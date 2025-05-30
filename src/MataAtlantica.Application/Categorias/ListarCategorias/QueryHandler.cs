﻿using MataAtlantica.Domain.Models.Categorias;
using MataAtlantica.Domain.Services;
using MediatR;

namespace MataAtlantica.Application.Categorias.ListarCategorias;

public record ListarCategoriasQuery : IRequest<List<CategoriaDto>> { }

public class QueryHandler(CategoriaService categoriaService) : IRequestHandler<ListarCategoriasQuery, List<CategoriaDto>>
{
    private readonly CategoriaService _categoriaService = categoriaService;
    public Task<List<CategoriaDto>> Handle(ListarCategoriasQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_categoriaService.ListarCategoriasComoArvore());
    }
}
