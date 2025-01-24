namespace MataAtlantica.Domain.Models.Usuarios;

public record UsuarioDto(string Id, string Nome, string Sobrenome, string Login, Endereco endereco);

public record AdicionarUsuarioDto(
    string UsuarioId,
    string Nome,
    string Sobrenome,
    string Login,
    Endereco Endereco);