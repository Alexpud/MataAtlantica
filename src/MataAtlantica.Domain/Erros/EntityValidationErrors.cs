using FluentResults;

namespace MataAtlantica.Domain.Erros;

public static class EntityValidationErrors
{
    public static IError BairroObrigatorioParaEndereco = new Error("Bairro e obrigatorio");
    public static IError NumeroObrigatorioParaEndereco = new Error("Numero e obrigatorio");
    public static IError RuaObrigatorioParaEndereco = new Error("Rua e obrigatorio");
    public static IError CidadeObrigatorioParaEndereco = new Error("Cidade e obrigatorio");
    public static IError EstadoObrigatorioParaEndereco = new Error("Estado e obrigatorio");
    public static IError CepObrigatorioParaEndereco = new Error("Cep e obrigatorio");
    public static IError UFDeveTerNoMaximo2CaracteresParaEndereco = new Error("UF deve ter no maximo 2 caracteres");

    public static IError TelefoneObrigatorioParaFornecedor = new Error("Telefone e obrigatorio");
    public static IError NomeObrigatorioParaFornecedor = new Error("Nome e obrigatorio");
    public static IError CpfCnpjObrigatorioParaFornecedor = new Error("Cpf e obrigatorio");
    public static IError DescricaoObrigatoriaParaFornecedor = new Error("Descricao e obrigatoria");

    public static IError DescricaoObrigatorioParaAvaliacao = new Error("Descricao e obrigatoria");
    public static IError NotaProdutoObrigatoriaParaAvaliacao = new Error("Nota do produto e obrigatoria");
    public static IError ProdutoObrigatorioParaAvaliacao = new Error("Produto e obrigatorio");

    public static IError NomeObrigatorioParaProduto = new ValidationError("NomeObrigatorioParaProduto", "Nome e obrigatorio");
    public static IError DescricaoObrigatorioParaProduto = new ValidationError("DescricaoObrigatorioParaProduto", "Descricao e obrigatoria");
    public static IError MarcaObrigatoriaParaProduto = new ValidationError("MarcarobrigatoriaParaProduto", "Marca e obrigatoria");
    public static IError PrecoObrigatorioParaProduto = new ValidationError("PrecoObrigatorioParaProduto", "Preco e obrigatorio");
    public static IError CategoriaObrigatorioParaProduto = new ValidationError("CategoriaObrigatorioParaProduto", "Categoria e obrigatorio");
    public static IError FornecedorObrigatorioParaProduto = new ValidationError("FornecedorObrigatorioParaProduto", "Fornecedor e obrigatorio");
    public static IError NomeObrigatorioParaCategoria = new ValidationError("NomeObrigatorioParaCategoria", "Nome e obrigatorio");
}

public static class BusinessErrors
{
    public static IError FornecedorNaoEncontrado = new ValidationError("FornecedorNaoEncontrado", "Fornecedor nao encontrado");
    public static IError FornecedorComCpfCnpjJaExiste = new ValidationError("FornecedorComCpfCnpjJaExiste", "Fornecedor com o cpf/cnpj informado ja existe");
    public static IError CategoriaNaoEncontrada = new ValidationError("CategoriaNaoEncontrada", "Categoria nao encontrada");
    public static IError NenhumImagemPassada = new ValidationError("NenhumaImagemPassada", "Nenhuma imagem foi passada");
    public static IError QuantidadeDeArquivosSuperiorAoLimite = new ValidationError("QuantidadeDeArquivosSuperiorAoLimite", "Quantidade de arquivos superior ao limite");
    public static IError ArquivoComFormatoInvalido = new ValidationError("ArquivoComFormatoInvalido", "Arquivo com formato invalido");
    public static IError ImagemMuitoGrande = new ValidationError("ImagemMuitoGrande", "Imagem ultrapassa o tamanho máximo de 1Mb");
    public static IError ProdutoNaoEncontrado = new ValidationError("ProdutoNaoEncontrado", "Produto nao encontrado");
    public static IError UsuarioComLoginJaExiste = new ValidationError("UsuarioComLoginJaExiste", "Usuario com com o login informado ja existe");
    public static IError UsuarioComLoginDesformatado = new ValidationError("UsuarioComLoginDesformatado", "Usuario não possui login no formato de email");
    public static IError SenhaObrigatoriaParaUsuario = new ValidationError("SenhaObrigatoriaParaUsuario", "Senha e obrigatoria");
    public static IError NomeObrigatorioParaUsuario = new ValidationError("NomeObrigatorioParaUsuario", "Nome e obrigatorio");
    public static IError SobrenomeObrigatorioParaUsuario = new ValidationError("SobrenomeObrigatorioParaUsuario", "Sobrenome e obrigatorio");
}

public class ValidationError : Error
{
    public ValidationError(string errorCode, string errorMessage) : base(errorMessage)
    {
        WithMetadata("ErrorCode", errorCode);
    }
}