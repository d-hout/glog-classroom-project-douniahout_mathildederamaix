using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediathequeWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adherents",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DateInscription = table.Column<DateOnly>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adherents", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Oeuvres",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    AuteurId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuteursId = table.Column<int>(type: "INTEGER", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oeuvres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oeuvres_Auteurs_AuteursId",
                        column: x => x.AuteursId,
                        principalTable: "Auteurs",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Exemplaires",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OeuvreId = table.Column<int>(type: "INTEGER", nullable: false),
                    Statut = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemplaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exemplaires_Oeuvres_OeuvreId",
                        column: x => x.OeuvreId,
                        principalTable: "Oeuvres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OeuvreId = table.Column<int>(type: "INTEGER", nullable: true),
                    AdherentId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateReservation = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Statut = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Reservations_Oeuvres_OeuvreId",
                        column: x => x.OeuvreId,
                        principalTable: "Oeuvres",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdherentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ExemplaireId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateEmprunt = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DateRetourPrevue = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DateRetourReelle = table.Column<DateOnly>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprunts_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Emprunts_Exemplaires_ExemplaireId",
                        column: x => x.ExemplaireId,
                        principalTable: "Exemplaires",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_AdherentId",
                table: "Emprunts",
                column: "AdherentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_ExemplaireId",
                table: "Emprunts",
                column: "ExemplaireId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Exemplaires_OeuvreId",
                table: "Exemplaires",
                column: "OeuvreId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Oeuvres_AuteursId",
                table: "Oeuvres",
                column: "AuteursId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AdherentId",
                table: "Reservations",
                column: "AdherentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OeuvreId",
                table: "Reservations",
                column: "OeuvreId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Emprunts");

            migrationBuilder.DropTable(name: "Reservations");

            migrationBuilder.DropTable(name: "Exemplaires");

            migrationBuilder.DropTable(name: "Adherents");

            migrationBuilder.DropTable(name: "Oeuvres");

            migrationBuilder.DropTable(name: "Auteurs");
        }
    }
}
