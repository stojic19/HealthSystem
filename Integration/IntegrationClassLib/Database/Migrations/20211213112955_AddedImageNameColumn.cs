using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class AddedImageNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pharmacies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Pharmacies",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Pharmacies");
        }
    }
}
