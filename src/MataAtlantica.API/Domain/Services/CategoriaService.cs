using AutoMapper;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Repositories.Abstract;

namespace MataAtlantica.API.Domain.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
    private readonly IMapper _mapper = mapper;

    public List<CategoriaDto> ListarCategoriasComoArvore()
    {
        var categorias = _categoriaRepository.AsQueryable().ToList();
        var categoriaDto = _mapper.Map<List<CategoriaDto>>(categorias);
        var result = new List<CategoriaDto>();
        foreach(var categoria in categoriaDto)
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

    public async Task<CategoriaDto> Adicionar(AdicionarCategoriaDto dto)
    {
        var categoria = new Categoria(dto.Nome);
        _categoriaRepository.Adicionar(categoria);
        await _categoriaRepository.Commit();
        return _mapper.Map<CategoriaDto>(categoria);
    }

    public async Task<CategoriaDto> AdicionarSubCategoria(string id, string nome)
    {
        var categoria = await _categoriaRepository.ObterPorId(id);
        var subCategoria = new Categoria(nome);
        subCategoria.SetCategoriaPai(categoria);
        _categoriaRepository.Adicionar(subCategoria);

        await _categoriaRepository.Commit();

        categoria = await _categoriaRepository.ObterPorId(id, categoria => categoria.SubCategorias);
        return _mapper.Map<CategoriaDto>(categoria);
    }
}