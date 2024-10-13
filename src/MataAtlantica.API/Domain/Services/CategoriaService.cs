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

    public async Task<CategoriaDto> AdicionarSubCategoria(string id, string nome)
    {
        var categoria = await _categoriaRepository.ObterPorId(id);
        if (categoria.EhSubCategoria())
            throw new Exception("Não se pode adicioanr subcategoria a uma subcategoria");

        var subCategoria = new Categoria(nome);
        subCategoria.SetCategoriaPai(categoria);
        _categoriaRepository.Adicionar(subCategoria);

        await _categoriaRepository.Commit();

        categoria = await _categoriaRepository.ObterPorId(id, categoria => categoria.SubCategorias);
        return _mapper.Map<CategoriaDto>(categoria);
    }
}