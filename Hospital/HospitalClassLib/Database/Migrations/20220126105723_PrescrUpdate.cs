using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class PrescrUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_ScheduledEvents_ScheduledEventId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduledEventId",
                table: "Prescriptions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_ScheduledEvents_ScheduledEventId",
                table: "Prescriptions",
                column: "ScheduledEventId",
                principalTable: "ScheduledEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_ScheduledEvents_ScheduledEventId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduledEventId",
                table: "Prescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_ScheduledEvents_ScheduledEventId",
                table: "Prescriptions",
                column: "ScheduledEventId",
                principalTable: "ScheduledEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
