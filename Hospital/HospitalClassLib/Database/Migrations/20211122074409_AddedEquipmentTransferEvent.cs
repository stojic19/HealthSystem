using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace Hospital.Migrations
{
    public partial class AddedEquipmentTransferEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomType",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EquipmentTransferEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InitalRoomId = table.Column<int>(type: "integer", nullable: true),
                    DestinationRoomId = table.Column<int>(type: "integer", nullable: true),
                    InventoryItemId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTransferEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTransferEvents_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentTransferEvents_Rooms_DestinationRoomId",
                        column: x => x.DestinationRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentTransferEvents_Rooms_InitalRoomId",
                        column: x => x.InitalRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransferEvents_DestinationRoomId",
                table: "EquipmentTransferEvents",
                column: "DestinationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransferEvents_InitalRoomId",
                table: "EquipmentTransferEvents",
                column: "InitalRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransferEvents_InventoryItemId",
                table: "EquipmentTransferEvents",
                column: "InventoryItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentTransferEvents");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Rooms");
        }
    }
}
