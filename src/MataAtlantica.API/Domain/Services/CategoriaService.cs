using AutoMapper;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;
using MataAtlantica.API.Domain.Specifications;

namespace MataAtlantica.API.Domain.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public List<CategoriaDto> ListarCategoriasComoArvore()
    {
        var categorias = _categoriaRepository.FilterBy(new CategoriasRaizSpecification().Expression, p => p.SubCategorias);

        return _mapper.Map<List<CategoriaDto>>(categorias);
    }

    public async Task<CategoriaDto> Adicionar(AdicionarCategoriaArgs dto)
    {
        var categoria = new Categoria(dto.Nome);
        _categoriaRepository.Adicionar(categoria);
        await _categoriaRepository.Commit();
        return _mapper.Map<CategoriaDto>(categoria);
    }
}