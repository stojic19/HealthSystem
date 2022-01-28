using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class RemovedManagerFromComplaints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Managers_ManagerId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ManagerId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Complaints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Complaints",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ManagerId",
                table: "Complaints",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Managers_ManagerId",
                table: "Complaints",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
