using FluentResults;
using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.Domain.Erros;

public static class EntityValidationErrors
{
    public static IError BairroObrigatorioParaEndereco = new Error("Bairro é obrigatório");
    public static IError NumeroObrigatorioParaEndereco = new Error("Número é obrigatório");
    public static IError RuaObrigatorioParaEndereco = new Error("Rua é obrigatória");
    public static IError CidadeObrigatorioParaEndereco = new Error("Cidade é obrigatória");
    public static IError EstadoObrigatorioParaEndereco = new Error("Estado é obrigatório");
    public static IError CepObrigatorioParaEndereco = new Error("CEP é obrigatório");
    public static IError UFDeveTerNoMaximo2CaracteresParaEndereco = new Error("UF deve ter no máximo 2 caracteres");

    public static IError TelefoneObrigatorioParaFornecedor = new Error("Telefone é obrigatório");
    public static IError NomeObrigatorioParaFornecedor = new Error("Nome é obrigatório");
    public static IError CpfCnpjObrigatorioParaFornecedor = new Error("CPF/CNPJ é obrigatório");
    public static IError DescricaoObrigatoriaParaFornecedor = new Error("Descrição é obrigatória");

    public static IError DescricaoObrigatorioParaAvaliacao = new Error("Descrição é obrigatória");
    public static IError NotaProdutoObrigatoriaParaAvaliacao = new Error("Nota do produto é obrigatória");
    public static IError ProdutoObrigatorioParaAvaliacao = new Error("Produto é obrigatório");

    public static IError NomeObrigatorioParaProduto = new ValidationError("NomeObrigatorioParaProduto", "Nome é obrigatório");
    public static IError DescricaoObrigatorioParaProduto = new ValidationError("DescricaoObrigatorioParaProduto", "Descrição é obrigatória");
    public static IError MarcaObrigatoriaParaProduto = new ValidationError("MarcaObrigatoriaParaProduto", "Marca é obrigatória");
    public static IError PrecoObrigatorioParaProduto = new ValidationError("PrecoObrigatorioParaProduto", "Preço é obrigatório");
    public static IError CategoriaObrigatorioParaProduto = new ValidationError("CategoriaObrigatorioParaProduto", "Categoria é obrigatória");
    public static IError FornecedorObrigatorioParaProduto = new ValidationError("FornecedorObrigatorioParaProduto", "Fornecedor é obrigatório");
    public static IError NomeObrigatorioParaCategoria = new ValidationError("NomeObrigatorioParaCategoria", "Nome é obrigatório");
}

public static class BusinessErrors
{
    public static IError FornecedorNaoEncontrado = new ValidationError("FornecedorNaoEncontrado", "Fornecedor não encontrado");
    public static IError FornecedorComCpfCnpjJaExiste = new ValidationError("FornecedorComCpfCnpjJaExiste", "Fornecedor com o CPF/CNPJ informado já existe");
    public static IError CategoriaNaoEncontrada = new ValidationError("CategoriaNaoEncontrada", "Categoria não encontrada");
    public static IError NenhumImagemPassada = new ValidationError("NenhumaImagemPassada", "Nenhuma imagem foi passada");
    public static IError QuantidadeDeArquivosSuperiorAoLimite = new ValidationError("QuantidadeDeArquivosSuperiorAoLimite", "Quantidade de arquivos superior ao limite");
    public static IError ArquivoComFormatoInvalido = new ValidationError("ArquivoComFormatoInvalido", "Arquivo com formato inválido");
    public static IError ImagemMuitoGrande = new ValidationError("ImagemMuitoGrande", "Imagem ultrapassa o tamanho máximo de 1Mb");
    public static IError ProdutoNaoEncontrado = new ValidationError("ProdutoNaoEncontrado", "Produto não encontrado");
    public static IError UsuarioComLoginJaExiste = new ValidationError("UsuarioComLoginJaExiste", "Usuário com o login informado já existe");
    public static IError UsuarioComLoginDesformatado = new ValidationError("UsuarioComLoginDesformatado", "Usuário não possui login no formato de email");
    public static IError SenhaObrigatoriaParaUsuario = new ValidationError("SenhaObrigatoriaParaUsuario", "Senha é obrigatória");
    public static IError NomeObrigatorioParaUsuario = new ValidationError("NomeObrigatorioParaUsuario", "Nome é obrigatório");
    public static IError SobrenomeObrigatorioParaUsuario = new ValidationError("SobrenomeObrigatorioParaUsuario", "Sobrenome é obrigatório");
    public static IError UsuarioNaoEncontrado = new ValidationError("UsuarioNaoEncontrado", "Usuário não encontrado");
    public static IError NumeroIdentificacaoObrigatorioParaMetodoPagamento = new ValidationError("NumeroIdentificacaoObrigatorioParaMetodoPagamento", "Número de identificação é obrigatório");
    public static IError ValidadeInferiorADataAtualParaMetodoPagamento = new ValidationError("ValidadeInferiorADataAtualParaMetodoPagamento", "Validade inferior à data atual");
    public static IError NumeroIdentificacaoInvalidoParaMetodoPagamento = new ValidationError("NumeroIdentificacaoInvalidoParaMetodoPagamento", "Número de identificação inválido");
    public static IError IdMetodoPagamentoObrigatorio = new ValidationError("IdMetodoPagamentoObrigatorio", "ID do método de pagamento é obrigatório");
    public static IError IdUsuarioObrigatorio = new ValidationError("IdUsuarioObrigatorio", "ID do usuário é obrigatório");
    public static IError MetodoPagamentoNaoEncontrado = new ValidationError("MetodoPagamentoNaoEncontrado", "Método de pagamento não encontrado");
}


public class ValidationError : Error
{
    public ValidationError(string errorCode, string errorMessage) : base(errorMessage)
    {
        WithMetadata("ErrorCode", errorCode);
    }
}