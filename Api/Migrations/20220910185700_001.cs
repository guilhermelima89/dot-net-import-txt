using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArquivoStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arquivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    ArquivoOriginal = table.Column<byte[]>(type: "varbinary(MAX)", nullable: false),
                    ArquivoRetorno = table.Column<byte[]>(type: "varbinary(MAX)", nullable: true),
                    ArquivoStatusId = table.Column<int>(type: "int", nullable: false),
                    DataInicioProcessamento = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataFimProcessamento = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivo_ArquivoStatus_ArquivoStatusId",
                        column: x => x.ArquivoStatusId,
                        principalTable: "ArquivoStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArquivoComErroValidacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArquivoId = table.Column<int>(type: "int", nullable: false),
                    NumeroLinha = table.Column<int>(type: "int", nullable: false),
                    TextoLinha = table.Column<string>(type: "varchar(250)", nullable: false),
                    Erro = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoComErroValidacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoComErroValidacao_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "Arquivo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArquivoSemErroValidacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArquivoId = table.Column<int>(type: "int", nullable: false),
                    Identificador = table.Column<string>(type: "varchar(250)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "varchar(250)", nullable: false),
                    NomeAcesso = table.Column<string>(type: "varchar(250)", nullable: false),
                    ContaPrincipal = table.Column<string>(type: "varchar(250)", nullable: false),
                    Confirmacao = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoSemErroValidacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoSemErroValidacao_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "Arquivo",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ArquivoStatus",
                columns: new[] { "Id", "DataAlteracao", "DataCadastro", "Descricao" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1639), "Aguardando processamento" },
                    { 2, null, new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1648), "Processado sem erros" },
                    { 3, null, new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1649), "Processado parcialmente com erros" },
                    { 4, null, new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1650), "Não processado" },
                    { 5, null, new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1651), "Processando" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_ArquivoStatusId",
                table: "Arquivo",
                column: "ArquivoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoComErroValidacao_ArquivoId",
                table: "ArquivoComErroValidacao",
                column: "ArquivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoSemErroValidacao_ArquivoId",
                table: "ArquivoSemErroValidacao",
                column: "ArquivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivoComErroValidacao");

            migrationBuilder.DropTable(
                name: "ArquivoSemErroValidacao");

            migrationBuilder.DropTable(
                name: "Arquivo");

            migrationBuilder.DropTable(
                name: "ArquivoStatus");
        }
    }
}
