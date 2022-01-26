using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdatedStoredEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "EventStoring",
                table: "StoredEvent",
                newName: "Step");

            migrationBuilder.RenameColumn(
                name: "StateData",
                schema: "EventStoring",
                table: "StoredEvent",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "EventStoring",
                table: "StoredEvent",
                newName: "StateData");

            migrationBuilder.RenameColumn(
                name: "Step",
                schema: "EventStoring",
                table: "StoredEvent",
                newName: "UserId");
        }
    }
}
