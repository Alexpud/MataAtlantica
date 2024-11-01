using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Entidades;

public class Fornecedor : EntidadeBase
{
    public Fornecedor()
    {
    }

    public Fornecedor(CriarFornecedor model)
    {
        Nome = model.Nome;
        Descricao = model.Descricao;
        CpfCnpj = model.CpfCnpj;
        Telefone = model.Telefone;
        Localizacao.Bairro = model.Localizacao.Bairro;
        Localizacao.CEP = model.Localizacao.CEP;
        Localizacao.UF = model.Localizacao.UF;
        Localizacao.Cidade = model.Localizacao.Cidade;
        Localizacao.Numero = model.Localizacao.Numero;
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Endereco Localizacao { get; set; } = new();
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public DateTime? UltimaAtualizacao { get; private set; }
    public List<Produto> Produtos { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; }
}

public class Endereco
{
    public string Rua { get; set; }
    public string Bairro { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public Fornecedor Fornecedor { get; set; }
}
