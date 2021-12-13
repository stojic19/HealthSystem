using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ScheduledEventUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_DoctorId",
                table: "ScheduledEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_PatientId",
                table: "ScheduledEvents");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "ScheduledEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "ScheduledEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                table: "ScheduledEvents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_DoctorId",
                table: "ScheduledEvents",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_PatientId",
                table: "ScheduledEvents",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_DoctorId",
                table: "ScheduledEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_PatientId",
                table: "ScheduledEvents");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                table: "ScheduledEvents");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "ScheduledEvents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "ScheduledEvents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_DoctorId",
                table: "ScheduledEvents",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_AspNetUsers_PatientId",
                table: "ScheduledEvents",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
