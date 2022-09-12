using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArquivoStatusProcessamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtLinhas = table.Column<int>(type: "int", nullable: false),
                    QtLinhasErros = table.Column<int>(type: "int", nullable: false),
                    QtLinhasProcessadas = table.Column<int>(type: "int", nullable: false),
                    ArquivoId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoStatusProcessamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoStatusProcessamento_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "Arquivo",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoStatusProcessamento_ArquivoId",
                table: "ArquivoStatusProcessamento",
                column: "ArquivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivoStatusProcessamento");

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1639));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1649));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "ArquivoStatus",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCadastro",
                value: new DateTime(2022, 9, 10, 15, 57, 0, 475, DateTimeKind.Local).AddTicks(1651));
        }
    }
}
