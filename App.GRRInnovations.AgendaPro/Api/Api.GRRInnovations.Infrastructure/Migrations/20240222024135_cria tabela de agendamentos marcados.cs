using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.GRRInnovations.Infrastructure.Migrations
{
    public partial class criatabeladeagendamentosmarcados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledAppointments",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParentUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledAppointments", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_ScheduledAppointments_Schedules_ParentUid",
                        column: x => x.ParentUid,
                        principalTable: "Schedules",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledAppointments_ParentUid",
                table: "ScheduledAppointments",
                column: "ParentUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledAppointments");
        }
    }
}
