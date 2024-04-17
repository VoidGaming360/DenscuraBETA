using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolisDensCuraBETA.repositories.Migrations
{
    /// <inheritdoc />
    public partial class appointmetupdatechangeiddatatype2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_DentistIdId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientIdId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DentistIdId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientIdId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DentistIdId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientIdId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "DentistId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DentistId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "DentistIdId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientIdId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DentistIdId",
                table: "Appointments",
                column: "DentistIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientIdId",
                table: "Appointments",
                column: "PatientIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_DentistIdId",
                table: "Appointments",
                column: "DentistIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientIdId",
                table: "Appointments",
                column: "PatientIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
