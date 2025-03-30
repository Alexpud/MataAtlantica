﻿// <auto-generated />
using System;
using MataAtlantica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MataAtlantica.Infrastructure.Data.Migrations
{
    [DbContext(typeof(MataAtlanticaDbContext))]
    [Migration("20250330153848_AdicionandoPedido")]
    partial class AdicionandoPedido
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Avaliacao", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("FornecedorId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<float>("NotaProduto")
                        .HasColumnType("real");

                    b.Property<string>("ProdutoId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime?>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Categoria", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CategoriaPaiId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPaiId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.InformacaoEntregaPedido", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntregaEstimada")
                        .HasColumnType("datetime2");

                    b.Property<string>("PedidoCompraID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Transportadora")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoCompraID")
                        .IsUnique()
                        .HasFilter("[PedidoCompraID] IS NOT NULL");

                    b.ToTable("InformacaoEntregaPedido");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.InformacaoPagamentoPedido", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("InformacoesPagamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PedidoCompraId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoCompraId")
                        .IsUnique()
                        .HasFilter("[PedidoCompraId] IS NOT NULL");

                    b.ToTable("InformacaoPagamentoPedido");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.PedidoCompra", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("InformacaoEntregaId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PedidoCompra");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.PedidoItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("PedidoCompraId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProdutoId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoCompraId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PedidoItem");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Fornecedor", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Produto", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CategoriaId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FornecedorId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Preco")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Avaliacao", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Fornecedor", null)
                        .WithMany("Avaliacoes")
                        .HasForeignKey("FornecedorId");

                    b.HasOne("MataAtlantica.Domain.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Categoria", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Categoria", "CategoriaPai")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaPaiId");

                    b.Navigation("CategoriaPai");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.InformacaoEntregaPedido", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Compras.PedidoCompra", "PedidoCompra")
                        .WithOne("InformacaoEntregaPedido")
                        .HasForeignKey("MataAtlantica.Domain.Entidades.Compras.InformacaoEntregaPedido", "PedidoCompraID");

                    b.Navigation("PedidoCompra");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.InformacaoPagamentoPedido", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Compras.PedidoCompra", "PedidoCompra")
                        .WithOne("InformacaoPagamento")
                        .HasForeignKey("MataAtlantica.Domain.Entidades.Compras.InformacaoPagamentoPedido", "PedidoCompraId");

                    b.Navigation("PedidoCompra");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.PedidoCompra", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.PedidoItem", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Compras.PedidoCompra", "PedidoCompra")
                        .WithMany("Items")
                        .HasForeignKey("PedidoCompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MataAtlantica.Domain.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

                    b.Navigation("PedidoCompra");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Fornecedor", b =>
                {
                    b.OwnsOne("MataAtlantica.Domain.Entidades.Endereco", "Localizacao", b1 =>
                        {
                            b1.Property<string>("FornecedorId")
                                .HasColumnType("nvarchar(36)");

                            b1.Property<string>("Bairro")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("CEP")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Cidade")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Numero")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Rua")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UF")
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)");

                            b1.HasKey("FornecedorId");

                            b1.ToTable("Fornecedor");

                            b1.WithOwner()
                                .HasForeignKey("FornecedorId");
                        });

                    b.Navigation("Localizacao");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Produto", b =>
                {
                    b.HasOne("MataAtlantica.Domain.Entidades.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.HasOne("MataAtlantica.Domain.Entidades.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId");

                    b.OwnsOne("MataAtlantica.Domain.Entidades.ConfiguracaoImagens", "ConfiguracaoImagens", b1 =>
                        {
                            b1.Property<string>("ProdutoId")
                                .HasColumnType("nvarchar(36)");

                            b1.HasKey("ProdutoId");

                            b1.ToTable("Produto");

                            b1.ToJson("ConfiguracaoImagens");

                            b1.WithOwner()
                                .HasForeignKey("ProdutoId");

                            b1.OwnsMany("MataAtlantica.Domain.Entidades.Imagem", "ImagensIlustrativas", b2 =>
                                {
                                    b2.Property<string>("ConfiguracaoImagensProdutoId")
                                        .HasColumnType("nvarchar(36)");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("Ordem")
                                        .HasColumnType("int");

                                    b2.HasKey("ConfiguracaoImagensProdutoId", "Id");

                                    b2.ToTable("Produto");

                                    b2.WithOwner()
                                        .HasForeignKey("ConfiguracaoImagensProdutoId");
                                });

                            b1.OwnsMany("MataAtlantica.Domain.Entidades.Imagem", "Thumbnails", b2 =>
                                {
                                    b2.Property<string>("ConfiguracaoImagensProdutoId")
                                        .HasColumnType("nvarchar(36)");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("Ordem")
                                        .HasColumnType("int");

                                    b2.HasKey("ConfiguracaoImagensProdutoId", "Id");

                                    b2.ToTable("Produto");

                                    b2.WithOwner()
                                        .HasForeignKey("ConfiguracaoImagensProdutoId");
                                });

                            b1.Navigation("ImagensIlustrativas");

                            b1.Navigation("Thumbnails");
                        });

                    b.Navigation("Categoria");

                    b.Navigation("ConfiguracaoImagens");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Usuario", b =>
                {
                    b.OwnsMany("MataAtlantica.Domain.Entidades.MetodoPagamento", "OpcoesPagamento", b1 =>
                        {
                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Bandeira")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("CriadoEm")
                                .HasColumnType("datetime2");

                            b1.Property<string>("NumeroIdentificacao")
                                .IsRequired()
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.Property<string>("Tipo")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("UltimaAtualizacao")
                                .HasColumnType("datetime2");

                            b1.Property<string>("UsuarioId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("Validade")
                                .HasColumnType("datetime2");

                            b1.HasKey("Id");

                            b1.HasIndex("UsuarioId");

                            b1.ToTable("MetodoPagamento", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("MataAtlantica.Domain.Entidades.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<string>("UsuarioId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Bairro")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("CEP")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Cidade")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Numero")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Rua")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("UF")
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Endereco");

                    b.Navigation("OpcoesPagamento");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Categoria", b =>
                {
                    b.Navigation("SubCategorias");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Compras.PedidoCompra", b =>
                {
                    b.Navigation("InformacaoEntregaPedido");

                    b.Navigation("InformacaoPagamento");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("MataAtlantica.Domain.Entidades.Fornecedor", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
