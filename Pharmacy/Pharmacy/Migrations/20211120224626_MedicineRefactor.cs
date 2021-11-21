using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pharmacy.Migrations
{
    public partial class MedicineRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPrecautions",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MedicinesThatCanBeCombined",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "PotentialDangers",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Reactions",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "SideEffects",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Substances",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "BaseUrl",
                table: "Hospitals",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicineCombinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstMedicineId = table.Column<int>(type: "integer", nullable: false),
                    SecondMedicineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineCombinations_Medicines_FirstMedicineId",
                        column: x => x.FirstMedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineCombinations_Medicines_SecondMedicineId",
                        column: x => x.SecondMedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicinePotentialDangers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePotentialDangers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Precautions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precautions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SideEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideEffects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Substances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineMedicinePotentialDanger",
                columns: table => new
                {
                    MedicinePotentialDangersId = table.Column<int>(type: "integer", nullable: false),
                    MedicinesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineMedicinePotentialDanger", x => new { x.MedicinePotentialDangersId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_MedicineMedicinePotentialDanger_MedicinePotentialDangers_Me~",
                        column: x => x.MedicinePotentialDangersId,
                        principalTable: "MedicinePotentialDangers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineMedicinePotentialDanger_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_MedicineCombinations_FirstMedicineId",
                table: "MedicineCombinations",
                column: "FirstMedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineCombinations_SecondMedicineId",
                table: "MedicineCombinations",
                column: "SecondMedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineMedicinePotentialDanger_MedicinesId",
                table: "MedicineMedicinePotentialDanger",
                column: "MedicinesId");

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
                name: "MedicineCombinations");

            migrationBuilder.DropTable(
                name: "MedicineMedicinePotentialDanger");

            migrationBuilder.DropTable(
                name: "MedicinePrecaution");

            migrationBuilder.DropTable(
                name: "MedicineReaction");

            migrationBuilder.DropTable(
                name: "MedicineSideEffect");

            migrationBuilder.DropTable(
                name: "MedicineSubstance");

            migrationBuilder.DropTable(
                name: "MedicinePotentialDangers");

            migrationBuilder.DropTable(
                name: "Precautions");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "SideEffects");

            migrationBuilder.DropTable(
                name: "Substances");

            migrationBuilder.DropColumn(
                name: "BaseUrl",
                table: "Hospitals");

            migrationBuilder.AddColumn<List<string>>(
                name: "MainPrecautions",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "MedicinesThatCanBeCombined",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "PotentialDangers",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Reactions",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "SideEffects",
                table: "Medicines",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Substances",
                table: "Medicines",
                type: "text[]",
                nullable: true);
        }
    }
}
