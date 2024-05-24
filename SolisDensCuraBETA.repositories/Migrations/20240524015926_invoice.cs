using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolisDensCuraBETA.repositories.Migrations
{
    /// <inheritdoc />
    public partial class invoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Treatments",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatus",
                table: "Costs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Treatments");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Costs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
