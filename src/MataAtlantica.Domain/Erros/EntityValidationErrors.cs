using FluentResults;
using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.Domain.Erros;

public static class EntityValidationErrors
{
    public static IError BairroObrigatorioParaEndereco = new Error("Bairro � obrigat�rio");
    public static IError NumeroObrigatorioParaEndereco = new Error("N�mero � obrigat�rio");
    public static IError RuaObrigatorioParaEndereco = new Error("Rua � obrigat�ria");
    public static IError CidadeObrigatorioParaEndereco = new Error("Cidade � obrigat�ria");
    public static IError EstadoObrigatorioParaEndereco = new Error("Estado � obrigat�rio");
    public static IError CepObrigatorioParaEndereco = new Error("CEP � obrigat�rio");
    public static IError UFDeveTerNoMaximo2CaracteresParaEndereco = new Error("UF deve ter no m�ximo 2 caracteres");

    public static IError TelefoneObrigatorioParaFornecedor = new Error("Telefone � obrigat�rio");
    public static IError NomeObrigatorioParaFornecedor = new Error("Nome � obrigat�rio");
    public static IError CpfCnpjObrigatorioParaFornecedor = new Error("CPF/CNPJ � obrigat�rio");
    public static IError DescricaoObrigatoriaParaFornecedor = new Error("Descri��o � obrigat�ria");

    public static IError DescricaoObrigatorioParaAvaliacao = new Error("Descri��o � obrigat�ria");
    public static IError NotaProdutoObrigatoriaParaAvaliacao = new Error("Nota do produto � obrigat�ria");
    public static IError ProdutoObrigatorioParaAvaliacao = new Error("Produto � obrigat�rio");

    public static IError NomeObrigatorioParaProduto = new ValidationError("NomeObrigatorioParaProduto", "Nome � obrigat�rio");
    public static IError DescricaoObrigatorioParaProduto = new ValidationError("DescricaoObrigatorioParaProduto", "Descri��o � obrigat�ria");
    public static IError MarcaObrigatoriaParaProduto = new ValidationError("MarcaObrigatoriaParaProduto", "Marca � obrigat�ria");
    public static IError PrecoObrigatorioParaProduto = new ValidationError("PrecoObrigatorioParaProduto", "Pre�o � obrigat�rio");
    public static IError CategoriaObrigatorioParaProduto = new ValidationError("CategoriaObrigatorioParaProduto", "Categoria � obrigat�ria");
    public static IError FornecedorObrigatorioParaProduto = new ValidationError("FornecedorObrigatorioParaProduto", "Fornecedor � obrigat�rio");
    public static IError NomeObrigatorioParaCategoria = new ValidationError("NomeObrigatorioParaCategoria", "Nome � obrigat�rio");
}

public static class BusinessErrors
{
    public static IError FornecedorNaoEncontrado = new ValidationError("FornecedorNaoEncontrado", "Fornecedor n�o encontrado");
    public static IError FornecedorComCpfCnpjJaExiste = new ValidationError("FornecedorComCpfCnpjJaExiste", "Fornecedor com o CPF/CNPJ informado j� existe");
    public static IError CategoriaNaoEncontrada = new ValidationError("CategoriaNaoEncontrada", "Categoria n�o encontrada");
    public static IError NenhumImagemPassada = new ValidationError("NenhumaImagemPassada", "Nenhuma imagem foi passada");
    public static IError QuantidadeDeArquivosSuperiorAoLimite = new ValidationError("QuantidadeDeArquivosSuperiorAoLimite", "Quantidade de arquivos superior ao limite");
    public static IError ArquivoComFormatoInvalido = new ValidationError("ArquivoComFormatoInvalido", "Arquivo com formato inv�lido");
    public static IError ImagemMuitoGrande = new ValidationError("ImagemMuitoGrande", "Imagem ultrapassa o tamanho m�ximo de 1Mb");
    public static IError ProdutoNaoEncontrado = new ValidationError("ProdutoNaoEncontrado", "Produto n�o encontrado");
    public static IError UsuarioComLoginJaExiste = new ValidationError("UsuarioComLoginJaExiste", "Usu�rio com o login informado j� existe");
    public static IError UsuarioComLoginDesformatado = new ValidationError("UsuarioComLoginDesformatado", "Usu�rio n�o possui login no formato de email");
    public static IError SenhaObrigatoriaParaUsuario = new ValidationError("SenhaObrigatoriaParaUsuario", "Senha � obrigat�ria");
    public static IError NomeObrigatorioParaUsuario = new ValidationError("NomeObrigatorioParaUsuario", "Nome � obrigat�rio");
    public static IError SobrenomeObrigatorioParaUsuario = new ValidationError("SobrenomeObrigatorioParaUsuario", "Sobrenome � obrigat�rio");
    public static IError UsuarioNaoEncontrado = new ValidationError("UsuarioNaoEncontrado", "Usu�rio n�o encontrado");
    public static IError NumeroIdentificacaoObrigatorioParaMetodoPagamento = new ValidationError("NumeroIdentificacaoObrigatorioParaMetodoPagamento", "N�mero de identifica��o � obrigat�rio");
    public static IError ValidadeInferiorADataAtualParaMetodoPagamento = new ValidationError("ValidadeInferiorADataAtualParaMetodoPagamento", "Validade inferior � data atual");
    public static IError NumeroIdentificacaoInvalidoParaMetodoPagamento = new ValidationError("NumeroIdentificacaoInvalidoParaMetodoPagamento", "N�mero de identifica��o inv�lido");
    public static IError IdMetodoPagamentoObrigatorio = new ValidationError("IdMetodoPagamentoObrigatorio", "ID do m�todo de pagamento � obrigat�rio");
    public static IError IdUsuarioObrigatorio = new ValidationError("IdUsuarioObrigatorio", "ID do usu�rio � obrigat�rio");
    public static IError MetodoPagamentoNaoEncontrado = new ValidationError("MetodoPagamentoNaoEncontrado", "M�todo de pagamento n�o encontrado");
}


public class ValidationError : Error
{
    public ValidationError(string errorCode, string errorMessage) : base(errorMessage)
    {
        WithMetadata("ErrorCode", errorCode);
    }
}