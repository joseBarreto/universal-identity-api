using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversalIdentity.Infra.Data.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TOTAL_AVALIACAO",
                table: "T_PESSOA",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TOTAL_HORAS_TRABALHADAS",
                table: "T_PESSOA",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TOTAL_AVALIACAO",
                table: "T_PESSOA");

            migrationBuilder.DropColumn(
                name: "TOTAL_HORAS_TRABALHADAS",
                table: "T_PESSOA");
        }
    }
}
