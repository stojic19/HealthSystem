using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class DoctorSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_InventoryItems_InventoryItemId",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_DestinationRoomId",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_InitialRoomId",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacation_AspNetUsers_DoctorId",
                table: "Vacation");

            migrationBuilder.DropTable(
                name: "DoctorOnCallDuty");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTransferEvents_DestinationRoomId",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropColumn(
                name: "DestinationRoomId",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "EquipmentTransferEvents");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Vacation",
                newName: "DoctorScheduleId");

            migrationBuilder.RenameColumn(
                name: "InventoryItemId",
                table: "EquipmentTransferEvents",
                newName: "InitialRoomInventoryId");

            migrationBuilder.RenameColumn(
                name: "InitialRoomId",
                table: "EquipmentTransferEvents",
                newName: "DestinationRoomInventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTransferEvents_InventoryItemId",
                table: "EquipmentTransferEvents",
                newName: "IX_EquipmentTransferEvents_InitialRoomInventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTransferEvents_InitialRoomId",
                table: "EquipmentTransferEvents",
                newName: "IX_EquipmentTransferEvents_DestinationRoomInventoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePeriod_EndTime",
                table: "EquipmentTransferEvents",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePeriod_StartTime",
                table: "EquipmentTransferEvents",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorScheduleId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorScheduleOnCallDuty",
                columns: table => new
                {
                    DoctorsOnDutyId = table.Column<int>(type: "integer", nullable: false),
                    OnCallDutiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorScheduleOnCallDuty", x => new { x.DoctorsOnDutyId, x.OnCallDutiesId });
                    table.ForeignKey(
                        name: "FK_DoctorScheduleOnCallDuty_DoctorSchedule_DoctorsOnDutyId",
                        column: x => x.DoctorsOnDutyId,
                        principalTable: "DoctorSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleOnCallDuty_OnCallDuties_OnCallDutiesId",
                        column: x => x.OnCallDutiesId,
                        principalTable: "OnCallDuties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorScheduleId",
                table: "AspNetUsers",
                column: "DoctorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleOnCallDuty_OnCallDutiesId",
                table: "DoctorScheduleOnCallDuty",
                column: "OnCallDutiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DoctorSchedule_DoctorScheduleId",
                table: "AspNetUsers",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_RoomInventories_DestinationRoomInve~",
                table: "EquipmentTransferEvents",
                column: "DestinationRoomInventoryId",
                principalTable: "RoomInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_RoomInventories_InitialRoomInventor~",
                table: "EquipmentTransferEvents",
                column: "InitialRoomInventoryId",
                principalTable: "RoomInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacation_DoctorSchedule_DoctorScheduleId",
                table: "Vacation",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DoctorSchedule_DoctorScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_RoomInventories_DestinationRoomInve~",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_RoomInventories_InitialRoomInventor~",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacation_DoctorSchedule_DoctorScheduleId",
                table: "Vacation");

            migrationBuilder.DropTable(
                name: "DoctorScheduleOnCallDuty");

            migrationBuilder.DropTable(
                name: "DoctorSchedule");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TimePeriod_EndTime",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropColumn(
                name: "TimePeriod_StartTime",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropColumn(
                name: "DoctorScheduleId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DoctorScheduleId",
                table: "Vacation",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "InitialRoomInventoryId",
                table: "EquipmentTransferEvents",
                newName: "InventoryItemId");

            migrationBuilder.RenameColumn(
                name: "DestinationRoomInventoryId",
                table: "EquipmentTransferEvents",
                newName: "InitialRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTransferEvents_InitialRoomInventoryId",
                table: "EquipmentTransferEvents",
                newName: "IX_EquipmentTransferEvents_InventoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTransferEvents_DestinationRoomInventoryId",
                table: "EquipmentTransferEvents",
                newName: "IX_EquipmentTransferEvents_InitialRoomId");

            migrationBuilder.AddColumn<int>(
                name: "DestinationRoomId",
                table: "EquipmentTransferEvents",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "EquipmentTransferEvents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "EquipmentTransferEvents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "IX_EquipmentTransferEvents_DestinationRoomId",
                table: "EquipmentTransferEvents",
                column: "DestinationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorOnCallDuty_OnCallDutiesId",
                table: "DoctorOnCallDuty",
                column: "OnCallDutiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_InventoryItems_InventoryItemId",
                table: "EquipmentTransferEvents",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_DestinationRoomId",
                table: "EquipmentTransferEvents",
                column: "DestinationRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_InitialRoomId",
                table: "EquipmentTransferEvents",
                column: "InitialRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacation_AspNetUsers_DoctorId",
                table: "Vacation",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
