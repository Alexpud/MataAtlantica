namespace MataAtlantica.Domain.Entidades;

public class Usuario : EntidadeBase
{
    public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public string Login { get; private set; }
    public DateTime UltimaAtualizacao { get; private set; }
    public Endereco Endereco { get; private set; }

    private Usuario()
    {
        
    }

    public Usuario(
        string id,
        string nome, 
        string sobrenome, 
        string login, 
        Endereco endereco)
    {
        Id = id;
        Nome = nome;
        Sobrenome = sobrenome;
        Login = login;
        Endereco = endereco;
    }
}
