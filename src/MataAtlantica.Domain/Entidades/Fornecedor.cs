using MataAtlantica.Domain.Models.Fornecedores;

namespace MataAtlantica.Domain.Entidades;

public class Fornecedor : EntidadeBase
{
    public Fornecedor()
    {
    }

    public Fornecedor(AdicionarFornecedorDto model)
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
        Localizacao.Rua = model.Localizacao.Rua;
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Endereco Localizacao { get; set; } = new();
    public string CpfCnpj { get; set; }
    public string Telefone { get; set; }
    public DateTime? UltimaAtualizacao { get; private set; }
    public List<Produto> Produtos { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; }

    public void AtualizarAPartirDe(AlterarFornecedorDto alterarFornecedor)
    {
        UltimaAtualizacao = DateTime.UtcNow;
        Nome = alterarFornecedor.Nome;
        Descricao = alterarFornecedor.Descricao;
        Telefone = alterarFornecedor.Telefone;
        CpfCnpj = alterarFornecedor.CpfCnpj;
        Localizacao.UF = alterarFornecedor.Localizacao.UF;
        Localizacao.Cidade = alterarFornecedor.Localizacao.Cidade;
        Localizacao.CEP = alterarFornecedor.Localizacao.CEP;
        Localizacao.Numero = alterarFornecedor.Localizacao.Numero;
        Localizacao.Bairro = alterarFornecedor.Localizacao.Bairro;
        Localizacao.Rua = alterarFornecedor.Localizacao.Rua;
    }
}
