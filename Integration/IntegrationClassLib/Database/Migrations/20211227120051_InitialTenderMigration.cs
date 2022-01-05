using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    public partial class InitialTenderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenderOffer_MedicationRequests",
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
                    table.PrimaryKey("PK_TenderOffer_MedicationRequests", x => new { x.TenderOfferId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ClosedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ActiveRange_StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActiveRange_EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WinningOfferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PharmacyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Cost_Amount = table.Column<double>(type: "double precision", nullable: true),
                    Cost_Currency = table.Column<int>(type: "integer", nullable: true),
                    IsWinning = table.Column<bool>(type: "boolean", nullable: false),
                    TenderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderOffer_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenderOffer_Tenders_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_TenderOffer_PharmacyId",
                table: "TenderOffer",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderOffer_TenderId",
                table: "TenderOffer",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_WinningOfferId",
                table: "Tenders",
                column: "WinningOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffer_MedicationRequests_TenderOffer_TenderOfferId",
                table: "TenderOffer_MedicationRequests",
                column: "TenderOfferId",
                principalTable: "TenderOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenders_TenderOffer_WinningOfferId",
                table: "Tenders",
                column: "WinningOfferId",
                principalTable: "TenderOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderOffer_Tenders_TenderId",
                table: "TenderOffer");

            migrationBuilder.DropTable(
                name: "TenderOffer_MedicationRequests");

            migrationBuilder.DropTable(
                name: "Tenders_MedicationRequests");

            migrationBuilder.DropTable(
                name: "Tenders");

            migrationBuilder.DropTable(
                name: "TenderOffer");
        }
    }
}
