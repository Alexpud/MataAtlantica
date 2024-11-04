using AutoMapper;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Profiles;

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
