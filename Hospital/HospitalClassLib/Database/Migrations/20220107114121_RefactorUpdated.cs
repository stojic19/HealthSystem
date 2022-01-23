using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RefactorUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "Measurements_Height",
                table: "MedicalRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Measurements_Weight",
                table: "MedicalRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "City_Country_Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City_Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "City_PostalCode",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization_Description",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization_Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalTreatments_MedicalRecordId",
                table: "HospitalTreatments",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalTreatments_RoomId",
                table: "HospitalTreatments",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MedicalRecordId",
                table: "AspNetUsers",
                column: "MedicalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MedicalRecords_MedicalRecordId",
                table: "AspNetUsers",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_PatientId",
                table: "Feedbacks",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalTreatments_MedicalRecords_MedicalRecordId",
                table: "HospitalTreatments",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalTreatments_Rooms_RoomId",
                table: "HospitalTreatments",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MedicalRecords_MedicalRecordId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_PatientId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalTreatments_MedicalRecords_MedicalRecordId",
                table: "HospitalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalTreatments_Rooms_RoomId",
                table: "HospitalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_DoctorId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_HospitalTreatments_MedicalRecordId",
                table: "HospitalTreatments");

            migrationBuilder.DropIndex(
                name: "IX_HospitalTreatments_RoomId",
                table: "HospitalTreatments");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MedicalRecordId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Measurements_Height",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Measurements_Weight",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "City_Country_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City_PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialization_Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialization_Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
