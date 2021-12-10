using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    public partial class MedicineSpecificationFilesAddedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicineName",
                table: "MedicineSpecificationFiles",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedDate",
                table: "MedicineSpecificationFiles",
                type: "timestamp without time zone",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineName",
                table: "MedicineSpecificationFiles");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                table: "MedicineSpecificationFiles");
        }
    }
}
