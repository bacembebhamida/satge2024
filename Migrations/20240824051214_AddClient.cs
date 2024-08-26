using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DossiersPrelevement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Formulaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossiersPrelevement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossiersPrelevement_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fichiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFichier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheminFichier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DossierPrelevementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fichiers_DossiersPrelevement_DossierPrelevementId",
                        column: x => x.DossierPrelevementId,
                        principalTable: "DossiersPrelevement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DossiersPrelevement_ClientId",
                table: "DossiersPrelevement",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Fichiers_DossierPrelevementId",
                table: "Fichiers",
                column: "DossierPrelevementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fichiers");

            migrationBuilder.DropTable(
                name: "DossiersPrelevement");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
