using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class PrescriptionUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduledEventId",
                table: "Prescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ScheduledEventId",
                table: "Prescriptions",
                column: "ScheduledEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_ScheduledEvents_ScheduledEventId",
                table: "Prescriptions",
                column: "ScheduledEventId",
                principalTable: "ScheduledEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_ScheduledEvents_ScheduledEventId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_ScheduledEventId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "ScheduledEventId",
                table: "Prescriptions");
        }
    }
}
