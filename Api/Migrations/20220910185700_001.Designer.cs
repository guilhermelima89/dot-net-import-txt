﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220910185700_001")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Api.Models.Arquivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("ArquivoOriginal")
                        .IsRequired()
                        .HasColumnType("varbinary(MAX)");

                    b.Property<byte[]>("ArquivoRetorno")
                        .HasColumnType("varbinary(MAX)");

                    b.Property<int>("ArquivoStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataFimProcessamento")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataInicioProcessamento")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ArquivoStatusId");

                    b.ToTable("Arquivo", (string)null);
                });

            modelBuilder.Entity("Api.Models.ArquivoComErroValidacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArquivoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Erro")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("NumeroLinha")
                        .HasColumnType("int");

                    b.Property<string>("TextoLinha")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ArquivoId");

                    b.ToTable("ArquivoComErroValidacao", (string)null);
                });

            modelBuilder.Entity("Api.Models.ArquivoSemErroValidacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArquivoId")
                        .HasColumnType("int");

                    b.Property<string>("Confirmacao")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ContaPrincipal")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NomeAcesso")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ArquivoId");

                    b.ToTable("ArquivoSemErroValidacao", (string)null);
                });

            modelBuilder.Entity("Api.Models.ArquivoStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("ArquivoStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataCadastro = new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1639),
                            Descricao = "Aguardando processamento"
                        },
                        new
                        {
                            Id = 2,
                            DataCadastro = new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1648),
                            Descricao = "Processado sem erros"
                        },
                        new
                        {
                            Id = 3,
                            DataCadastro = new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1649),
                            Descricao = "Processado parcialmente com erros"
                        },
                        new
                        {
                            Id = 4,
                            DataCadastro = new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1650),
                            Descricao = "Não processado"
                        },
                        new
                        {
                            Id = 5,
                            DataCadastro = new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1651),
                            Descricao = "Processando"
                        });
                });

            modelBuilder.Entity("Api.Models.Arquivo", b =>
                {
                    b.HasOne("Api.Models.ArquivoStatus", "ArquivoStatus")
                        .WithMany()
                        .HasForeignKey("ArquivoStatusId")
                        .IsRequired();

                    b.Navigation("ArquivoStatus");
                });

            modelBuilder.Entity("Api.Models.ArquivoComErroValidacao", b =>
                {
                    b.HasOne("Api.Models.Arquivo", "Arquivo")
                        .WithMany()
                        .HasForeignKey("ArquivoId")
                        .IsRequired();

                    b.Navigation("Arquivo");
                });

            modelBuilder.Entity("Api.Models.ArquivoSemErroValidacao", b =>
                {
                    b.HasOne("Api.Models.Arquivo", "Arquivo")
                        .WithMany()
                        .HasForeignKey("ArquivoId")
                        .IsRequired();

                    b.Navigation("Arquivo");
                });
#pragma warning restore 612, 618
        }
    }
}