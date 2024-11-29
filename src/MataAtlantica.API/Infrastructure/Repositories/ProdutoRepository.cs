using MataAtlantica.API.Domain.Abstract.Repositories;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Specifications;
using MataAtlantica.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.API.Infrastructure.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(MataAtlanticaDbContext dbContext) : base(dbContext)
    {
        
    }

    public IQueryable<ProdutoDto> Buscar(BuscarProdutosArgs args)
    {
        var spec = new BuscarProdutosSpecification(args);
        var produtos =  BuscarPorSpec(spec);

        return produtos
            .AsNoTracking()
            .Select(p => new ProdutoDto
        {
            Id = p.Id,
            Categoria = p.Categoria.Nome,
            Nome = p.Nome,  
            Descricao = p.Descricao,
            Marca = p.Marca,
            NomeFornecedor = p.Fornecedor.Nome,
            Preco = p.Preco,
            ConfiguracaoImagens = p.ConfiguracaoImagens
        });
    }
}
