using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolisDensCuraBETA.repositories.Migrations
{
    /// <inheritdoc />
    public partial class invoice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPressed",
                table: "Treatments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPressed",
                table: "Treatments");
        }
    }
}
