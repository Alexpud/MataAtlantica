using AutoMapper;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models;

namespace MataAtlantica.Domain.Profiles;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<Categoria, CategoriaDto>().PreserveReferences();
        CreateMap<Endereco, EnderecoFornecedor>();
        CreateMap<Fornecedor, FornecedorDto>();
        CreateMap<Produto, ProdutoDto>()
            .ForMember(p => p.NomeFornecedor, p => p.MapFrom(m => m.Fornecedor.Nome))
            .ForMember(p => p.Categoria, p => p.MapFrom(m => m.Categoria.Nome));
    }
}
