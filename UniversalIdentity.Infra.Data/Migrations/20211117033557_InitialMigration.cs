using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace UniversalIdentity.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_PESSOA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NOME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GENERO = table.Column<int>(type: "int", nullable: false),
                    DOCUMENTO_NUMERO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DOCUMENTO_TIPO = table.Column<int>(type: "int", nullable: false),
                    DOCUMENTO_DT_EMISSAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PESSOA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_LOGIN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PESSOA_ID = table.Column<int>(type: "int", nullable: false),
                    DT_ULTIMO_ACESSO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SENHA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LOGIN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_LOGIN_T_PESSOA_PESSOA_ID",
                        column: x => x.PESSOA_ID,
                        principalTable: "T_PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_LOGIN_PESSOA_ID",
                table: "T_LOGIN",
                column: "PESSOA_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LOGIN");

            migrationBuilder.DropTable(
                name: "T_PESSOA");
        }
    }
}
