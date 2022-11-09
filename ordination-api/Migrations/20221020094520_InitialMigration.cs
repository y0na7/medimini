using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ordination_api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Laegemiddler",
                columns: table => new
                {
                    LaegemiddelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    navn = table.Column<string>(type: "TEXT", nullable: false),
                    enhedPrKgPrDoegnLet = table.Column<double>(type: "REAL", nullable: false),
                    enhedPrKgPrDoegnNormal = table.Column<double>(type: "REAL", nullable: false),
                    enhedPrKgPrDoegnTung = table.Column<double>(type: "REAL", nullable: false),
                    enhed = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laegemiddler", x => x.LaegemiddelId);
                });

            migrationBuilder.CreateTable(
                name: "Patienter",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cprnr = table.Column<string>(type: "TEXT", nullable: false),
                    navn = table.Column<string>(type: "TEXT", nullable: false),
                    vaegt = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patienter", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Dato",
                columns: table => new
                {
                    DatoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PNOrdinationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dato", x => x.DatoId);
                });

            migrationBuilder.CreateTable(
                name: "Dosis",
                columns: table => new
                {
                    DosisId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tid = table.Column<DateTime>(type: "TEXT", nullable: false),
                    antal = table.Column<double>(type: "REAL", nullable: false),
                    DagligSkævOrdinationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosis", x => x.DosisId);
                });

            migrationBuilder.CreateTable(
                name: "Ordinationer",
                columns: table => new
                {
                    OrdinationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    startDen = table.Column<DateTime>(type: "TEXT", nullable: false),
                    slutDen = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LaegemiddelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: true),
                    MorgenDosisDosisId = table.Column<int>(type: "INTEGER", nullable: true),
                    MiddagDosisDosisId = table.Column<int>(type: "INTEGER", nullable: true),
                    AftenDosisDosisId = table.Column<int>(type: "INTEGER", nullable: true),
                    NatDosisDosisId = table.Column<int>(type: "INTEGER", nullable: true),
                    antalEnheder = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordinationer", x => x.OrdinationId);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Dosis_AftenDosisDosisId",
                        column: x => x.AftenDosisDosisId,
                        principalTable: "Dosis",
                        principalColumn: "DosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Dosis_MiddagDosisDosisId",
                        column: x => x.MiddagDosisDosisId,
                        principalTable: "Dosis",
                        principalColumn: "DosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Dosis_MorgenDosisDosisId",
                        column: x => x.MorgenDosisDosisId,
                        principalTable: "Dosis",
                        principalColumn: "DosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Dosis_NatDosisDosisId",
                        column: x => x.NatDosisDosisId,
                        principalTable: "Dosis",
                        principalColumn: "DosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Laegemiddler_LaegemiddelId",
                        column: x => x.LaegemiddelId,
                        principalTable: "Laegemiddler",
                        principalColumn: "LaegemiddelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Patienter_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patienter",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dato_PNOrdinationId",
                table: "Dato",
                column: "PNOrdinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dosis_DagligSkævOrdinationId",
                table: "Dosis",
                column: "DagligSkævOrdinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_AftenDosisDosisId",
                table: "Ordinationer",
                column: "AftenDosisDosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_LaegemiddelId",
                table: "Ordinationer",
                column: "LaegemiddelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_MiddagDosisDosisId",
                table: "Ordinationer",
                column: "MiddagDosisDosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_MorgenDosisDosisId",
                table: "Ordinationer",
                column: "MorgenDosisDosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_NatDosisDosisId",
                table: "Ordinationer",
                column: "NatDosisDosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_PatientId",
                table: "Ordinationer",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dato_Ordinationer_PNOrdinationId",
                table: "Dato",
                column: "PNOrdinationId",
                principalTable: "Ordinationer",
                principalColumn: "OrdinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dosis_Ordinationer_DagligSkævOrdinationId",
                table: "Dosis",
                column: "DagligSkævOrdinationId",
                principalTable: "Ordinationer",
                principalColumn: "OrdinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dosis_Ordinationer_DagligSkævOrdinationId",
                table: "Dosis");

            migrationBuilder.DropTable(
                name: "Dato");

            migrationBuilder.DropTable(
                name: "Ordinationer");

            migrationBuilder.DropTable(
                name: "Dosis");

            migrationBuilder.DropTable(
                name: "Laegemiddler");

            migrationBuilder.DropTable(
                name: "Patienter");
        }
    }
}
