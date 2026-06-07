using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPort.Backend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLidStateToSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentLidPosition",
                table: "Sensors",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLidPosition",
                table: "Sensors");
        }
    }
}
