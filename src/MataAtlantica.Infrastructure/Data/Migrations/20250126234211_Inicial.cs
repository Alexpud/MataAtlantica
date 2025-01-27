using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MataAtlantica.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CategoriaPaiId = table.Column<string>(type: "character varying(36)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Categoria_CategoriaPaiId",
                        column: x => x.CategoriaPaiId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Localizacao_Rua = table.Column<string>(type: "text", nullable: true),
                    Localizacao_Bairro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Localizacao_Numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Localizacao_Cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Localizacao_UF = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Localizacao_CEP = table.Column<string>(type: "text", nullable: true),
                    CpfCnpj = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Endereco_Rua = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Endereco_Bairro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Endereco_Numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Endereco_Cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Endereco_UF = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Endereco_CEP = table.Column<string>(type: "text", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Marca = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    CategoriaId = table.Column<string>(type: "character varying(36)", nullable: true),
                    FornecedorId = table.Column<string>(type: "character varying(36)", nullable: true),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ConfiguracaoImagens = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MetodoPagamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Bandeira = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Validade = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumeroIdentificacao = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetodoPagamento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    NotaProduto = table.Column<float>(type: "real", nullable: false),
                    ProdutoId = table.Column<string>(type: "character varying(36)", nullable: true),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FornecedorId = table.Column<string>(type: "character varying(36)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacao_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_FornecedorId",
                table: "Avaliacao",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ProdutoId",
                table: "Avaliacao",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_CategoriaPaiId",
                table: "Categoria",
                column: "CategoriaPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodoPagamento_UsuarioId",
                table: "MetodoPagamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_FornecedorId",
                table: "Produto",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Login",
                table: "Usuario",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "MetodoPagamento");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
