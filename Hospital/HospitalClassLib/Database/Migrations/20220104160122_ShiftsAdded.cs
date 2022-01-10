using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class ShiftsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomPositions");

            migrationBuilder.AddColumn<double>(
                name: "RoomPosition_DimensionX",
                table: "Rooms",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RoomPosition_DimensionY",
                table: "Rooms",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RoomPosition_Height",
                table: "Rooms",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RoomPosition_Width",
                table: "Rooms",
                type: "double precision",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OnCallDuties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallDuties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<int>(type: "integer", nullable: false),
                    To = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacation",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation", x => new { x.DoctorId, x.Id });
                    table.ForeignKey(
                        name: "FK_Vacation_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorOnCallDuty",
                columns: table => new
                {
                    DoctorsOnDutyId = table.Column<int>(type: "integer", nullable: false),
                    OnCallDutiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorOnCallDuty", x => new { x.DoctorsOnDutyId, x.OnCallDutiesId });
                    table.ForeignKey(
                        name: "FK_DoctorOnCallDuty_AspNetUsers_DoctorsOnDutyId",
                        column: x => x.DoctorsOnDutyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorOnCallDuty_OnCallDuties_OnCallDutiesId",
                        column: x => x.OnCallDutiesId,
                        principalTable: "OnCallDuties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShiftId",
                table: "AspNetUsers",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorOnCallDuty_OnCallDutiesId",
                table: "DoctorOnCallDuty",
                column: "OnCallDutiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Shifts_ShiftId",
                table: "AspNetUsers",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Shifts_ShiftId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DoctorOnCallDuty");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Vacation");

            migrationBuilder.DropTable(
                name: "OnCallDuties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShiftId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomPosition_DimensionX",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomPosition_DimensionY",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomPosition_Height",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomPosition_Width",
                table: "Rooms");

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

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "RoomPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DimensionX = table.Column<double>(type: "double precision", nullable: false),
                    DimensionY = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomPositions_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPositions_RoomId",
                table: "RoomPositions",
                column: "RoomId");
        }
    }
}
