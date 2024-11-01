using FluentResults;

namespace MataAtlantica.API.Domain.Erros;

public static class EntityValidationErrors
{
    public static IError BairroObrigatorioParaEndereco = new Error("Bairro e obrigatorio");
    public static IError NumeroObrigatorioParaEndereco = new Error("Numero e obrigatorio");
    public static IError RuaObrigatorioParaEndereco = new Error("Rua e obrigatorio");
    public static IError CidadeObrigatorioParaEndereco = new Error("Cidade e obrigatorio");
    public static IError EstadoObrigatorioParaEndereco = new Error("Estado e obrigatorio");
    public static IError CepObrigatorioParaEndereco = new Error("Cep e obrigatorio");
    
    public static IError TelefoneObrigatorioParaFornecedor = new Error("Telefone e obrigatorio");
    public static IError NomeObrigatorioParaFornecedor = new Error("Nome e obrigatorio");
    public static IError CpfCnpjObrigatorioParaFornecedor = new Error("Cpf e obrigatorio");
    public static IError DescricaoObrigatoriaParaFornecedor = new Error("Descricao e obrigatoria");
    
    public static IError DescricaoObrigatorioParaAvaliacao = new Error("Descricao e obrigatoria");
    public static IError NotaProdutoObrigatoriaParaAvaliacao = new Error("Nota do produto e obrigatoria");
    public static IError ProdutoObrigatorioParaAvaliacao = new Error("Produto e obrigatorio");
    
    public static IError NomeObrigatorioParaProduto = new Error("Nome e obrigatorio");
    public static IError DescricaoObrigatorioParaProduto = new Error("Descricao e obrigatoria");
    public static IError MarcaObrigatoriaParaProduto = new Error("Marca e obrigatoria");
    public static IError PrecoObrigatorioParaProduto = new Error("Preco e obrigatorio");
    public static IError CategoriaObrigatorioParaProduto = new Error("Categoria e obrigatorio");
    public static IError FornecedorObrigatorioParaProduto = new Error("Fornecedor e obrigatorio");
}

public static class BusinessErrors
{
    public static IError FornecedorNaoEncontrado = new Error("Fornecedor nao encontrado");
    public static IError FornecedorComCpfCnpjJaExiste = new Error("Fornecedor com o cpf/cnpj informado ja existe");
}