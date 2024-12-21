using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MataAtlantica.API.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Localizacao_Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localizacao_Bairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Localizacao_Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Localizacao_Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Localizacao_UF = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Localizacao_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpfCnpj = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    CategoriaId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    FornecedorId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    UltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfiguracaoImagens = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NotaProduto = table.Column<float>(type: "real", nullable: false),
                    ProdutoId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    UltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FornecedorId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_FornecedorId",
                table: "Produto",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
