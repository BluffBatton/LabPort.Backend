using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPort.Backend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ReferenceMax",
                table: "TestTypes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ReferenceMin",
                table: "TestTypes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "TestTypes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SensorReadingId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_SensorReadings_SensorReadingId",
                        column: x => x.SensorReadingId,
                        principalTable: "SensorReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_SensorReadingId",
                table: "Alerts",
                column: "SensorReadingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropColumn(
                name: "ReferenceMax",
                table: "TestTypes");

            migrationBuilder.DropColumn(
                name: "ReferenceMin",
                table: "TestTypes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "TestTypes");
        }
    }
}
