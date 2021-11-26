using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class AddedHospitalInReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "MedicineReportFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicineReportFiles_HospitalId",
                table: "MedicineReportFiles",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineReportFiles_Hospitals_HospitalId",
                table: "MedicineReportFiles",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineReportFiles_Hospitals_HospitalId",
                table: "MedicineReportFiles");

            migrationBuilder.DropIndex(
                name: "IX_MedicineReportFiles_HospitalId",
                table: "MedicineReportFiles");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "MedicineReportFiles");
        }
    }
}
