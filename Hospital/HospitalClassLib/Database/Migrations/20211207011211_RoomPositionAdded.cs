using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class RoomPositionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_InitalRoomId",
                table: "EquipmentTransferEvents");

            migrationBuilder.RenameColumn(
                name: "DimensionY",
                table: "Rooms",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "DimensionX",
                table: "Rooms",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "InitalRoomId",
                table: "EquipmentTransferEvents",
                newName: "InitialRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTransferEvents_InitalRoomId",
                table: "EquipmentTransferEvents",
                newName: "IX_EquipmentTransferEvents_InitialRoomId");

            migrationBuilder.CreateTable(
                name: "RoomPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    DimensionX = table.Column<double>(type: "double precision", nullable: false),
                    DimensionY = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RoomRenovationEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: true),
                    IsMerge = table.Column<bool>(type: "boolean", nullable: false),
                    MergeRoomId = table.Column<int>(type: "integer", nullable: true),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRenovationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomRenovationEvents_Rooms_MergeRoomId",
                        column: x => x.MergeRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomRenovationEvents_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPositions_RoomId",
                table: "RoomPositions",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRenovationEvents_MergeRoomId",
                table: "RoomRenovationEvents",
                column: "MergeRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRenovationEvents_RoomId",
                table: "RoomRenovationEvents",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_InitialRoomId",
                table: "EquipmentTransferEvents",
                column: "InitialRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_InitialRoomId",
                table: "EquipmentTransferEvents");

            migrationBuilder.DropTable(
                name: "RoomPositions");

            migrationBuilder.DropTable(
                name: "RoomRenovationEvents");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Rooms",
                newName: "DimensionY");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Rooms",
                newName: "DimensionX");

            migrationBuilder.RenameColumn(
                name: "InitialRoomId",
                table: "EquipmentTransferEvents",
                newName: "InitalRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTransferEvents_InitialRoomId",
                table: "EquipmentTransferEvents",
                newName: "IX_EquipmentTransferEvents_InitalRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTransferEvents_Rooms_InitalRoomId",
                table: "EquipmentTransferEvents",
                column: "InitalRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
