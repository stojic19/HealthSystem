using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class TenderWinningOfferFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenders_TenderOffer_WinningOfferId",
                table: "Tenders");

            migrationBuilder.AlterColumn<int>(
                name: "WinningOfferId",
                table: "Tenders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenders_TenderOffer_WinningOfferId",
                table: "Tenders",
                column: "WinningOfferId",
                principalTable: "TenderOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenders_TenderOffer_WinningOfferId",
                table: "Tenders");

            migrationBuilder.AlterColumn<int>(
                name: "WinningOfferId",
                table: "Tenders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenders_TenderOffer_WinningOfferId",
                table: "Tenders",
                column: "WinningOfferId",
                principalTable: "TenderOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
