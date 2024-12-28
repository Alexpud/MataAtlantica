using AutoMapper;
using FluentResults;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Domain.Specifications;

namespace MataAtlantica.Domain.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository,
    IMapper mapper)
{
    private readonly IMapper _mapper = mapper;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<Result<UsuarioDto>> Adicionar(AdicionarUsuarioDto dto)
    {
        var usuarioExistente = await _usuarioRepository.ObterPorSpec(new BuscarUsuarioPorLoginSpecification(dto.Login));
        if (usuarioExistente != null)
            return Result.Fail(BusinessErrors.UsuarioComLoginJaExiste);

        var endereco = new Endereco()
        {
            UF = dto.Endereco.UF,
            Bairro = dto.Endereco.Bairro,
            CEP = dto.Endereco.CEP,
            Cidade = dto.Endereco.Cidade,
            Numero = dto.Endereco.Numero,
            Rua = dto.Endereco.Rua,
        };
        var usuario = new Usuario(
            dto.UsuarioId,
            dto.Nome,
            dto.Sobrenome,
            dto.Login,
            endereco);

        _usuarioRepository.Adicionar(usuario);
        await _usuarioRepository.Commit();

        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        return Result.Ok(usuarioDto);
    }
}
