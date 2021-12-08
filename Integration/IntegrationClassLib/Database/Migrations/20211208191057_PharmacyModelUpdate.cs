using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class PharmacyModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GrpcSupported",
                table: "Pharmacies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrpcSupported",
                table: "Pharmacies");
        }
    }
}
