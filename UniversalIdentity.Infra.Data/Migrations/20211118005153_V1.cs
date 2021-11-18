using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversalIdentity.Infra.Data.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "T_PESSOA",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SENHA",
                table: "T_PESSOA",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMAIL",
                table: "T_PESSOA");

            migrationBuilder.DropColumn(
                name: "SENHA",
                table: "T_PESSOA");
        }
    }
}
