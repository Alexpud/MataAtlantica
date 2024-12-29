using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models;

public record AdicionarUsuarioRequest(
    [Required(AllowEmptyStrings = false)][EmailAddress] string Login,
    [Required(AllowEmptyStrings = false)] string Nome,
    [Required(AllowEmptyStrings = false)] string Sobrenome,
    [Required(AllowEmptyStrings = false)] Endereco Endereco,
    [Required(AllowEmptyStrings = false)] string Senha);
