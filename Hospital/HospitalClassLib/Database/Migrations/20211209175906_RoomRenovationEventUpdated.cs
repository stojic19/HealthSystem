using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RoomRenovationEventUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "RoomRenovationEvents");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "RoomRenovationEvents");

            migrationBuilder.AddColumn<string>(
                name: "FirstRoomDescription",
                table: "RoomRenovationEvents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstRoomName",
                table: "RoomRenovationEvents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstRoomType",
                table: "RoomRenovationEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecondRoomDescription",
                table: "RoomRenovationEvents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondRoomName",
                table: "RoomRenovationEvents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondRoomType",
                table: "RoomRenovationEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstRoomDescription",
                table: "RoomRenovationEvents");

            migrationBuilder.DropColumn(
                name: "FirstRoomName",
                table: "RoomRenovationEvents");

            migrationBuilder.DropColumn(
                name: "FirstRoomType",
                table: "RoomRenovationEvents");

            migrationBuilder.DropColumn(
                name: "SecondRoomDescription",
                table: "RoomRenovationEvents");

            migrationBuilder.DropColumn(
                name: "SecondRoomName",
                table: "RoomRenovationEvents");

            migrationBuilder.DropColumn(
                name: "SecondRoomType",
                table: "RoomRenovationEvents");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "RoomRenovationEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "RoomRenovationEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
