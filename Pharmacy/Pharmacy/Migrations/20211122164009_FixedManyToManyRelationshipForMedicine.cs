using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class FixedManyToManyRelationshipForMedicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Precautions_Medicines_MedicineId",
                table: "Precautions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Medicines_MedicineId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SideEffects_Medicines_MedicineId",
                table: "SideEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_Substances_Medicines_MedicineId",
                table: "Substances");

            migrationBuilder.DropIndex(
                name: "IX_Substances_MedicineId",
                table: "Substances");

            migrationBuilder.DropIndex(
                name: "IX_SideEffects_MedicineId",
                table: "SideEffects");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_MedicineId",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Precautions_MedicineId",
                table: "Precautions");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Substances");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "SideEffects");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Precautions");

            migrationBuilder.CreateTable(
                name: "MedicinePrecaution",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "integer", nullable: false),
                    PrecautionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePrecaution", x => new { x.MedicinesId, x.PrecautionsId });
                    table.ForeignKey(
                        name: "FK_MedicinePrecaution_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinePrecaution_Precautions_PrecautionsId",
                        column: x => x.PrecautionsId,
                        principalTable: "Precautions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineReaction",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "integer", nullable: false),
                    ReactionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineReaction", x => new { x.MedicinesId, x.ReactionsId });
                    table.ForeignKey(
                        name: "FK_MedicineReaction_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineReaction_Reactions_ReactionsId",
                        column: x => x.ReactionsId,
                        principalTable: "Reactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineSideEffect",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "integer", nullable: false),
                    SideEffectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineSideEffect", x => new { x.MedicinesId, x.SideEffectsId });
                    table.ForeignKey(
                        name: "FK_MedicineSideEffect_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineSideEffect_SideEffects_SideEffectsId",
                        column: x => x.SideEffectsId,
                        principalTable: "SideEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineSubstance",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "integer", nullable: false),
                    SubstancesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineSubstance", x => new { x.MedicinesId, x.SubstancesId });
                    table.ForeignKey(
                        name: "FK_MedicineSubstance_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineSubstance_Substances_SubstancesId",
                        column: x => x.SubstancesId,
                        principalTable: "Substances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePrecaution_PrecautionsId",
                table: "MedicinePrecaution",
                column: "PrecautionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineReaction_ReactionsId",
                table: "MedicineReaction",
                column: "ReactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineSideEffect_SideEffectsId",
                table: "MedicineSideEffect",
                column: "SideEffectsId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineSubstance_SubstancesId",
                table: "MedicineSubstance",
                column: "SubstancesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicinePrecaution");

            migrationBuilder.DropTable(
                name: "MedicineReaction");

            migrationBuilder.DropTable(
                name: "MedicineSideEffect");

            migrationBuilder.DropTable(
                name: "MedicineSubstance");

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Substances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "SideEffects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Reactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Precautions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Substances_MedicineId",
                table: "Substances",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_SideEffects_MedicineId",
                table: "SideEffects",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_MedicineId",
                table: "Reactions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Precautions_MedicineId",
                table: "Precautions",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Precautions_Medicines_MedicineId",
                table: "Precautions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Medicines_MedicineId",
                table: "Reactions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SideEffects_Medicines_MedicineId",
                table: "SideEffects",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Substances_Medicines_MedicineId",
                table: "Substances",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
