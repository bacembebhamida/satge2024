using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class DossierPrelevement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DossiersPrelevement_Clients_ClientId",
                table: "DossiersPrelevement");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "DossiersPrelevement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DossiersPrelevement_Clients_ClientId",
                table: "DossiersPrelevement",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DossiersPrelevement_Clients_ClientId",
                table: "DossiersPrelevement");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "DossiersPrelevement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DossiersPrelevement_Clients_ClientId",
                table: "DossiersPrelevement",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
