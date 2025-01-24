using MataAtlantica.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models.Usuarios;

public record AdicionarMetodoPagamentoRequest(
    [Required]BandeiraCartao Bandeira,
    [Required]string NumeroIdentificacao,
    [Required]DateTime Validade,
    [Required]TipoPagamento Tipo);

public record AlterarMetodoPagamentoRequest(
    [Required] string MetodoPagamentoId,
    [Required] BandeiraCartao Bandeira,
    [Required] string NumeroIdentificacao,
    [Required] DateTime Validade,
    [Required] TipoPagamento Tipo);