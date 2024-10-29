using AutoMapper;
using MataAtlantica.API.Domain.Entidades;
using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Profiles;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<Categoria, CategoriaDto>().PreserveReferences();
        CreateMap<Fornecedor, FornecedorDto>();
    }
}
