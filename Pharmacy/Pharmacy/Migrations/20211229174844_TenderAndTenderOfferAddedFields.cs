using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pharmacy.Migrations
{
    public partial class TenderAndTenderOfferAddedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffer_Hospitals_HospitalId",
                table: "TenderOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffer_Medicines_MedicineId",
                table: "TenderOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenderOffer",
                table: "TenderOffer");

            migrationBuilder.RenameTable(
                name: "TenderOffer",
                newName: "TenderOffers");

            migrationBuilder.RenameIndex(
                name: "IX_TenderOffer_MedicineId",
                table: "TenderOffers",
                newName: "IX_TenderOffers_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_TenderOffer_HospitalId",
                table: "TenderOffers",
                newName: "IX_TenderOffers_HospitalId");

            migrationBuilder.AddColumn<double>(
                name: "Cost_Amount",
                table: "TenderOffers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cost_Currency",
                table: "TenderOffers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenderId",
                table: "TenderOffers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenderOffers",
                table: "TenderOffers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TenderOffers_MedicationRequests",
                columns: table => new
                {
                    TenderOfferId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineName = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderOffers_MedicationRequests", x => new { x.TenderOfferId, x.Id });
                    table.ForeignKey(
                        name: "FK_TenderOffers_MedicationRequests_TenderOffers_TenderOfferId",
                        column: x => x.TenderOfferId,
                        principalTable: "TenderOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HospitalId = table.Column<int>(type: "integer", nullable: false),
                    ActiveRange_StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActiveRange_EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenders_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenders_MedicationRequests",
                columns: table => new
                {
                    TenderId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineName = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders_MedicationRequests", x => new { x.TenderId, x.Id });
                    table.ForeignKey(
                        name: "FK_Tenders_MedicationRequests_Tenders_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenderOffers_TenderId",
                table: "TenderOffers",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_HospitalId",
                table: "Tenders",
                column: "HospitalId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffers_Tenders_TenderId",
                table: "TenderOffers",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Hospitals_HospitalId",
                table: "TenderOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Medicines_MedicineId",
                table: "TenderOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffers_Tenders_TenderId",
                table: "TenderOffers");

            migrationBuilder.DropTable(
                name: "TenderOffers_MedicationRequests");

            migrationBuilder.DropTable(
                name: "Tenders_MedicationRequests");

            migrationBuilder.DropTable(
                name: "Tenders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenderOffers",
                table: "TenderOffers");

            migrationBuilder.DropIndex(
                name: "IX_TenderOffers_TenderId",
                table: "TenderOffers");

            migrationBuilder.DropColumn(
                name: "Cost_Amount",
                table: "TenderOffers");

            migrationBuilder.DropColumn(
                name: "Cost_Currency",
                table: "TenderOffers");

            migrationBuilder.DropColumn(
                name: "TenderId",
                table: "TenderOffers");

            migrationBuilder.RenameTable(
                name: "TenderOffers",
                newName: "TenderOffer");

            migrationBuilder.RenameIndex(
                name: "IX_TenderOffers_MedicineId",
                table: "TenderOffer",
                newName: "IX_TenderOffer_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_TenderOffers_HospitalId",
                table: "TenderOffer",
                newName: "IX_TenderOffer_HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenderOffer",
                table: "TenderOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffer_Hospitals_HospitalId",
                table: "TenderOffer",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffer_Medicines_MedicineId",
                table: "TenderOffer",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
