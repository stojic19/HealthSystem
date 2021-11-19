using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pharmacy.Migrations
{
    public partial class MedicineModelInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "MainPrecautions",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Medicines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<string>>(
                name: "MedicinesThatCanBeCombined",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "PotentialDangers",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Reactions",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "SideEffects",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Substances",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Medicines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "Medicines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Medicines",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_ManufacturerId",
                table: "Medicines",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Manufacturers_ManufacturerId",
                table: "Medicines",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Manufacturers_ManufacturerId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_ManufacturerId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MainPrecautions",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MedicinesThatCanBeCombined",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "PotentialDangers",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Reactions",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "SideEffects",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Substances",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Medicines");
        }
    }
}
