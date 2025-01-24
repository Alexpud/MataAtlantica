using AutoMapper;
using FluentResults;
using FluentValidation;
using MataAtlantica.Domain.Abstract.Repositories;
using MataAtlantica.Domain.Entidades;
using MataAtlantica.Domain.Erros;
using MataAtlantica.Domain.Helpers;
using MataAtlantica.Domain.Models.Usuarios;
using MataAtlantica.Domain.Specifications;

namespace MataAtlantica.Domain.Services;

public class UsuarioService(
    IUsuarioRepository usuarioRepository,
    IMapper mapper,
    IValidator<AdicionarMetodoPagamentoDto> adicionarMetodoPagamentoValidator,
    IValidator<AlterarMetodoPagamentoDto> alterarMetodoPagamentoValidator)
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AdicionarMetodoPagamentoDto> _adicionarMetodoPagamentoValidator = adicionarMetodoPagamentoValidator;
    private readonly IValidator<AlterarMetodoPagamentoDto> _alterarMetodoPagamentoValidator = alterarMetodoPagamentoValidator;
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

    public async Task<Result> AdicionarMetodoPagamento(AdicionarMetodoPagamentoDto dto)
    {
        var validationResult = await _adicionarMetodoPagamentoValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.GetErrors());

        var usuario = await _usuarioRepository.ObterPorId(dto.UsuarioId);
        var metodoPagamento = new MetodoPagamento(
            dto.Bandeira,
            dto.NumeroIdentificacao,
            dto.Validade,
            dto.Tipo);
        usuario.AdicionarMetodoPagamento(metodoPagamento);
        await _usuarioRepository.Commit();
        return Result.Ok();
    }

    public async Task<Result> AlterarMetodoPagamento(AlterarMetodoPagamentoDto dto)
    {
        var validationResult = await _alterarMetodoPagamentoValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.GetErrors());

        var usuario = await _usuarioRepository.ObterPorId(dto.UsuarioId, p => p.OpcoesPagamento);
        var metodoPagamento = new MetodoPagamento(
            dto.Bandeira,
            dto.NumeroIdentificacao,
            dto.Validade,
            dto.Tipo);
        var opcaoPagamento = usuario.OpcoesPagamento.First(p => p.Id == dto.MetodoPagamentoId);
        opcaoPagamento.AtualizarAPartir(dto);
        await _usuarioRepository.Commit();
        return Result.Ok();
    }
}
