using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class NewSurveyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalTreatments_MedicalRecords_MedicalRecordId",
                table: "HospitalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_AnsweredSurveys_AnsweredSurveyId",
                table: "ScheduledEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledEvents_AnsweredSurveyId",
                table: "ScheduledEvents");

            migrationBuilder.DropColumn(
                name: "AnsweredSurveyId",
                table: "ScheduledEvents");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalRecordId",
                table: "HospitalTreatments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "HospitalTreatments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalTreatments_MedicalRecords_MedicalRecordId",
                table: "HospitalTreatments",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnsweredSurveys_ScheduledEvents_ScheduledEventId",
                table: "AnsweredSurveys");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalTreatments_MedicalRecords_MedicalRecordId",
                table: "HospitalTreatments");

            migrationBuilder.DropIndex(
                name: "IX_AnsweredSurveys_ScheduledEventId",
                table: "AnsweredSurveys");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "HospitalTreatments");

            migrationBuilder.DropColumn(
                name: "ScheduledEventId",
                table: "AnsweredSurveys");

            migrationBuilder.AddColumn<int>(
                name: "AnsweredSurveyId",
                table: "ScheduledEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MedicalRecordId",
                table: "HospitalTreatments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledEvents_AnsweredSurveyId",
                table: "ScheduledEvents",
                column: "AnsweredSurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalTreatments_MedicalRecords_MedicalRecordId",
                table: "HospitalTreatments",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
