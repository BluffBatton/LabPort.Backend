using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPort.Backend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorReadings_Containers_ContainerId",
                table: "SensorReadings");

            migrationBuilder.RenameColumn(
                name: "ContainerId",
                table: "SensorReadings",
                newName: "SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorReadings_ContainerId",
                table: "SensorReadings",
                newName: "IX_SensorReadings_SensorId");

            migrationBuilder.CreateTable(
                name: "Sensord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeviceKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensord_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensord_ContainerId",
                table: "Sensord",
                column: "ContainerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorReadings_Sensord_SensorId",
                table: "SensorReadings",
                column: "SensorId",
                principalTable: "Sensord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorReadings_Sensord_SensorId",
                table: "SensorReadings");

            migrationBuilder.DropTable(
                name: "Sensord");

            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "SensorReadings",
                newName: "ContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorReadings_SensorId",
                table: "SensorReadings",
                newName: "IX_SensorReadings_ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorReadings_Containers_ContainerId",
                table: "SensorReadings",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
