using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MataAtlantica.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoCompra",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InformacaoEntregaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoCompra_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InformacaoEntregaPedido",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntregaEstimada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Transportadora = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PedidoCompraID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacaoEntregaPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacaoEntregaPedido_PedidoCompra_PedidoCompraID",
                        column: x => x.PedidoCompraID,
                        principalTable: "PedidoCompra",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InformacaoPagamentoPedido",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PedidoCompraId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InformacoesPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacaoPagamentoPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacaoPagamentoPedido_PedidoCompra_PedidoCompraId",
                        column: x => x.PedidoCompraId,
                        principalTable: "PedidoCompra",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PedidoItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdutoId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    PedidoCompraId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItem_PedidoCompra_PedidoCompraId",
                        column: x => x.PedidoCompraId,
                        principalTable: "PedidoCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItem_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformacaoEntregaPedido_PedidoCompraID",
                table: "InformacaoEntregaPedido",
                column: "PedidoCompraID",
                unique: true,
                filter: "[PedidoCompraID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InformacaoPagamentoPedido_PedidoCompraId",
                table: "InformacaoPagamentoPedido",
                column: "PedidoCompraId",
                unique: true,
                filter: "[PedidoCompraId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCompra_UsuarioId",
                table: "PedidoCompra",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItem_PedidoCompraId",
                table: "PedidoItem",
                column: "PedidoCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItem_ProdutoId",
                table: "PedidoItem",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformacaoEntregaPedido");

            migrationBuilder.DropTable(
                name: "InformacaoPagamentoPedido");

            migrationBuilder.DropTable(
                name: "PedidoItem");

            migrationBuilder.DropTable(
                name: "PedidoCompra");
        }
    }
}
