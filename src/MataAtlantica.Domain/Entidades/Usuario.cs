using MataAtlantica.Domain.Models.Usuarios;

namespace MataAtlantica.Domain.Entidades;

public class Usuario : EntidadeBase
{
    public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public string Login { get; private set; }
    public DateTime UltimaAtualizacao { get; private set; }
    public Endereco Endereco { get; private set; }
    public IEnumerable<MetodoPagamento> OpcoesPagamento = Enumerable.Empty<MetodoPagamento>();
    private Usuario() { }

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

    public void AdicionarMetodoPagamento(MetodoPagamento metodoPagamento) 
        => OpcoesPagamento.Append(metodoPagamento);
}

public class MetodoPagamento : EntidadeBase
{
    public BandeiraCartao Bandeira { get; private set; }
    public TipoPagamento Tipo { get; private set; }
    public DateTime Validade { get; private set; }
    public string NumeroIdentificacao { get; private set; }
    public string UsuarioId { get; private set; }
    public DateTime UltimaAtualizacao { get; private set; }
    public MetodoPagamento(
        BandeiraCartao bandeira, 
        string numeroIdentificacao, 
        DateTime validade, 
        TipoPagamento tipo)
    {
        Bandeira = bandeira;
        NumeroIdentificacao = numeroIdentificacao;
        Validade = validade;
        Tipo = tipo;
    }

    public void AtualizarAPartir(AlterarMetodoPagamentoDto dto)
    {
        Bandeira = dto.Bandeira;
        Tipo = dto.Tipo;
        Validade = dto.Validade;
        NumeroIdentificacao = dto.NumeroIdentificacao;
        UltimaAtualizacao = DateTime.Now;
    }
}

public enum TipoPagamento
{
    Credito,
    Debito
}

public enum BandeiraCartao
{
    Visa,
    MasterCard,
    Elo,
    AmericanExpress
}