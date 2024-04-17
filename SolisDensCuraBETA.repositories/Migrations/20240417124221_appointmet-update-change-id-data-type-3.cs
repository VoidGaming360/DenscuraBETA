using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolisDensCuraBETA.repositories.Migrations
{
    /// <inheritdoc />
    public partial class appointmetupdatechangeiddatatype3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DentistId",
                table: "Appointments",
                newName: "SelectedDentistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedDentistId",
                table: "Appointments",
                newName: "DentistId");
        }
    }
}
