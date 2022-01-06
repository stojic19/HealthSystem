using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class DoctorUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorsScheduleReport_EndTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorsScheduleReport_NumOfAppointments",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorsScheduleReport_NumOfOnCallShifts",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorsScheduleReport_NumOfPatients",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorsScheduleReport_StartTime",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DoctorsScheduleReport_EndTime",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorsScheduleReport_NumOfAppointments",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorsScheduleReport_NumOfOnCallShifts",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorsScheduleReport_NumOfPatients",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DoctorsScheduleReport_StartTime",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
