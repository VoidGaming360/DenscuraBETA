using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolisDensCuraBETA.repositories.Migrations
{
    /// <inheritdoc />
    public partial class Treatment_convert_decimal_costs_into_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_AspNetUsers_ApplicationUserId",
                table: "Treatment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treatment",
                table: "Treatment");

            migrationBuilder.RenameTable(
                name: "Treatment",
                newName: "Treatments");

            migrationBuilder.RenameIndex(
                name: "IX_Treatment_ApplicationUserId",
                table: "Treatments",
                newName: "IX_Treatments_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Costs",
                table: "Treatments",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treatments",
                table: "Treatments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_AspNetUsers_ApplicationUserId",
                table: "Treatments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_AspNetUsers_ApplicationUserId",
                table: "Treatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treatments",
                table: "Treatments");

            migrationBuilder.RenameTable(
                name: "Treatments",
                newName: "Treatment");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_ApplicationUserId",
                table: "Treatment",
                newName: "IX_Treatment_ApplicationUserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Costs",
                table: "Treatment",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treatment",
                table: "Treatment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_AspNetUsers_ApplicationUserId",
                table: "Treatment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
