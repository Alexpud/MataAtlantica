using AutoMapper;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Models.Categorias;
using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Models.Usuarios;

namespace MataAtlantica.Domain.Profiles;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<Categoria, CategoriaDto>().PreserveReferences();
        CreateMap<Entidades.Endereco, Models.Endereco>();
        CreateMap<Fornecedor, FornecedorDto>();
        CreateMap<Produto, ProdutoDto>()
            .ForMember(p => p.NomeFornecedor, p => p.MapFrom(m => m.Fornecedor.Nome))
            .ForMember(p => p.Categoria, p => p.MapFrom(m => m.Categoria.Nome));

        CreateMap<Usuario, UsuarioDto>();
    }
}
