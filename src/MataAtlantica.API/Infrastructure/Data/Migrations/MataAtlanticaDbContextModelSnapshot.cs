﻿// <auto-generated />
using System;
using MataAtlantica.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MataAtlantica.API.Infrastructure.Data.Migrations
{
    [DbContext(typeof(MataAtlanticaDbContext))]
    partial class MataAtlanticaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Avaliacao", b =>
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

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Categoria", b =>
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

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Fornecedor", b =>
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

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Produto", b =>
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

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Avaliacao", b =>
                {
                    b.HasOne("MataAtlantica.API.Domain.Entidades.Fornecedor", null)
                        .WithMany("Avaliacoes")
                        .HasForeignKey("FornecedorId");

                    b.HasOne("MataAtlantica.API.Domain.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Categoria", b =>
                {
                    b.HasOne("MataAtlantica.API.Domain.Entidades.Categoria", "CategoriaPai")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaPaiId");

                    b.Navigation("CategoriaPai");
                });

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Fornecedor", b =>
                {
                    b.OwnsOne("MataAtlantica.API.Domain.Entidades.Endereco", "Localizacao", b1 =>
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

                            b1.WithOwner("Fornecedor")
                                .HasForeignKey("FornecedorId");

                            b1.Navigation("Fornecedor");
                        });

                    b.Navigation("Localizacao");
                });

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Produto", b =>
                {
                    b.HasOne("MataAtlantica.API.Domain.Entidades.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.HasOne("MataAtlantica.API.Domain.Entidades.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId");

                    b.OwnsOne("MataAtlantica.API.Domain.Entidades.ConfiguracaoImagens", "ConfiguracaoImagens", b1 =>
                        {
                            b1.Property<string>("ProdutoId")
                                .HasColumnType("nvarchar(36)");

                            b1.HasKey("ProdutoId");

                            b1.ToTable("Produto");

                            b1.ToJson("ConfiguracaoImagens");

                            b1.WithOwner()
                                .HasForeignKey("ProdutoId");

                            b1.OwnsMany("MataAtlantica.API.Domain.Entidades.Imagem", "ImagensIlustrativas", b2 =>
                                {
                                    b2.Property<string>("ConfiguracaoImagensProdutoId")
                                        .HasColumnType("nvarchar(36)");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Nome")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("Ordem")
                                        .HasColumnType("int");

                                    b2.HasKey("ConfiguracaoImagensProdutoId", "Id");

                                    b2.ToTable("Produto");

                                    b2.WithOwner()
                                        .HasForeignKey("ConfiguracaoImagensProdutoId");
                                });

                            b1.OwnsMany("MataAtlantica.API.Domain.Entidades.Imagem", "Thumbnails", b2 =>
                                {
                                    b2.Property<string>("ConfiguracaoImagensProdutoId")
                                        .HasColumnType("nvarchar(36)");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Nome")
                                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Categoria", b =>
                {
                    b.Navigation("SubCategorias");
                });

            modelBuilder.Entity("MataAtlantica.API.Domain.Entidades.Fornecedor", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
