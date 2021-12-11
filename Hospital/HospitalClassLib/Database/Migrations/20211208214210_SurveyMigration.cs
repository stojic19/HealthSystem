using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class SurveyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_AnsweredSurveys_AnsweredSurveyId",
                table: "ScheduledEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledEvents_AnsweredSurveyId",
                table: "ScheduledEvents");

            migrationBuilder.DropColumn(
                name: "AnsweredSurveyId",
                table: "ScheduledEvents");

            migrationBuilder.AddColumn<int>(
                name: "ScheduledEventId",
                table: "AnsweredSurveys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredSurveys_ScheduledEventId",
                table: "AnsweredSurveys",
                column: "ScheduledEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnsweredSurveys_ScheduledEvents_ScheduledEventId",
                table: "AnsweredSurveys",
                column: "ScheduledEventId",
                principalTable: "ScheduledEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnsweredSurveys_ScheduledEvents_ScheduledEventId",
                table: "AnsweredSurveys");

            migrationBuilder.DropIndex(
                name: "IX_AnsweredSurveys_ScheduledEventId",
                table: "AnsweredSurveys");

            migrationBuilder.DropColumn(
                name: "ScheduledEventId",
                table: "AnsweredSurveys");

            migrationBuilder.AddColumn<int>(
                name: "AnsweredSurveyId",
                table: "ScheduledEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledEvents_AnsweredSurveyId",
                table: "ScheduledEvents",
                column: "AnsweredSurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_AnsweredSurveys_AnsweredSurveyId",
                table: "ScheduledEvents",
                column: "AnsweredSurveyId",
                principalTable: "AnsweredSurveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
