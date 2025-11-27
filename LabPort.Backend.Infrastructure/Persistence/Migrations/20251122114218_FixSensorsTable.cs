using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPort.Backend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixSensorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensord_Containers_ContainerId",
                table: "Sensord");

            migrationBuilder.DropForeignKey(
                name: "FK_SensorReadings_Sensord_SensorId",
                table: "SensorReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensord",
                table: "Sensord");

            migrationBuilder.RenameTable(
                name: "Sensord",
                newName: "Sensors");

            migrationBuilder.RenameIndex(
                name: "IX_Sensord_ContainerId",
                table: "Sensors",
                newName: "IX_Sensors_ContainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorReadings_Sensors_SensorId",
                table: "SensorReadings",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Containers_ContainerId",
                table: "Sensors",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorReadings_Sensors_SensorId",
                table: "SensorReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Containers_ContainerId",
                table: "Sensors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors");

            migrationBuilder.RenameTable(
                name: "Sensors",
                newName: "Sensord");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_ContainerId",
                table: "Sensord",
                newName: "IX_Sensord_ContainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensord",
                table: "Sensord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensord_Containers_ContainerId",
                table: "Sensord",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorReadings_Sensord_SensorId",
                table: "SensorReadings",
                column: "SensorId",
                principalTable: "Sensord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
