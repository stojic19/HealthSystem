using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class TenderAndTenderOfferRemovedUnusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Hospitals_HospitalId",
                table: "TenderOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Medicines_MedicineId",
                table: "TenderOffers");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TenderOffers");

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "TenderOffers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "TenderOffers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffers_Hospitals_HospitalId",
                table: "TenderOffers",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffers_Medicines_MedicineId",
                table: "TenderOffers",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Hospitals_HospitalId",
                table: "TenderOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Medicines_MedicineId",
                table: "TenderOffers");

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "TenderOffers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "TenderOffers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TenderOffers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffers_Hospitals_HospitalId",
                table: "TenderOffers",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffers_Medicines_MedicineId",
                table: "TenderOffers",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
