using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.GRRInnovations.Infrastructure.Migrations
{
    public partial class adicionandocamponome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schedules",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schedules");
        }
    }
}
