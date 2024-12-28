using AutoMapper;
using FluentResults;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Categorias;

namespace MataAtlantica.Domain.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public List<CategoriaDto> ListarCategoriasComoArvore()
    {
        var categorias = _categoriaRepository.AsQueryable().ToList();
        var categoriaDto = _mapper.Map<List<CategoriaDto>>(categorias);
        var result = new List<CategoriaDto>();
        foreach (var categoria in categoriaDto)
        {
            if (EhSubCategoria(categoria))
            {
                var categoriaPai = categoriaDto.FirstOrDefault(p => p.Id == categoria.CategoriaPaiId);
                categoriaPai.SubCategorias.Add(categoria);
            }
            else
            {
                result.Add(categoria);
            }
        }

        return result;
    }

    private static bool EhSubCategoria(CategoriaDto categoria)
        => categoria.CategoriaPaiId != null;

    public async Task<Result<Categoria>> AdicionarSubCategoria(string categoriaPaiId, string nome)
    {
        var categoria = await _categoriaRepository.ObterPorId(categoriaPaiId);
        if (categoria == null)
            return Result.Fail(BusinessErrors.CategoriaNaoEncontrada);

        var subCategoria = new Categoria(nome);
        subCategoria.SetCategoriaPai(categoria);
        return Result.Ok(subCategoria);
    }
}