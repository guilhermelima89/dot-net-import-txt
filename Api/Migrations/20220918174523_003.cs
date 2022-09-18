using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identificador",
                table: "ArquivoSemErroValidacao",
                newName: "TextoLinhaArquivoOriginal");

            migrationBuilder.RenameColumn(
                name: "TextoLinha",
                table: "ArquivoComErroValidacao",
                newName: "TextoLinhaArquivoOriginal");

            migrationBuilder.RenameColumn(
                name: "NumeroLinha",
                table: "ArquivoComErroValidacao",
                newName: "NumeroLinhaArquivoOriginal");

            migrationBuilder.AddColumn<int>(
                name: "NumeroLinhaArquivoOriginal",
                table: "ArquivoSemErroValidacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArquivoErro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroLinhaArquivoOriginal = table.Column<int>(type: "int", nullable: false),
                    TextoLinhaArquivoOriginal = table.Column<string>(type: "varchar(250)", nullable: false),
                    Erro = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime", nullable: false),
                    ArquivoId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoErro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoErro_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "Arquivo",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 18, 14, 45, 23, 69, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 18, 14, 45, 23, 69, DateTimeKind.Local).AddTicks(4662));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 18, 14, 45, 23, 69, DateTimeKind.Local).AddTicks(4664));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 18, 14, 45, 23, 69, DateTimeKind.Local).AddTicks(4665));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 18, 14, 45, 23, 69, DateTimeKind.Local).AddTicks(4666));

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoErro_ArquivoId",
                table: "ArquivoErro",
                column: "ArquivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivoErro");

            migrationBuilder.DropColumn(
                name: "NumeroLinhaArquivoOriginal",
                table: "ArquivoSemErroValidacao");

            migrationBuilder.RenameColumn(
                name: "TextoLinhaArquivoOriginal",
                table: "ArquivoSemErroValidacao",
                newName: "Identificador");

            migrationBuilder.RenameColumn(
                name: "TextoLinhaArquivoOriginal",
                table: "ArquivoComErroValidacao",
                newName: "TextoLinha");

            migrationBuilder.RenameColumn(
                name: "NumeroLinhaArquivoOriginal",
                table: "ArquivoComErroValidacao",
                newName: "NumeroLinha");

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 11, 14, 8, 47, 436, DateTimeKind.Local).AddTicks(2954));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 11, 14, 8, 47, 436, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 11, 14, 8, 47, 436, DateTimeKind.Local).AddTicks(2963));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 11, 14, 8, 47, 436, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 11, 14, 8, 47, 436, DateTimeKind.Local).AddTicks(2965));
        }
    }
}
