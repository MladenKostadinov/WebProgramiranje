using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knjige_Biblioteke_BibliotekaOveLokacijeID",
                table: "Knjige");

            migrationBuilder.RenameColumn(
                name: "BibliotekaOveLokacijeID",
                table: "Knjige",
                newName: "BibliotekaOveKnjigeID");

            migrationBuilder.RenameIndex(
                name: "IX_Knjige_BibliotekaOveLokacijeID",
                table: "Knjige",
                newName: "IX_Knjige_BibliotekaOveKnjigeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knjige_Biblioteke_BibliotekaOveKnjigeID",
                table: "Knjige",
                column: "BibliotekaOveKnjigeID",
                principalTable: "Biblioteke",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knjige_Biblioteke_BibliotekaOveKnjigeID",
                table: "Knjige");

            migrationBuilder.RenameColumn(
                name: "BibliotekaOveKnjigeID",
                table: "Knjige",
                newName: "BibliotekaOveLokacijeID");

            migrationBuilder.RenameIndex(
                name: "IX_Knjige_BibliotekaOveKnjigeID",
                table: "Knjige",
                newName: "IX_Knjige_BibliotekaOveLokacijeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knjige_Biblioteke_BibliotekaOveLokacijeID",
                table: "Knjige",
                column: "BibliotekaOveLokacijeID",
                principalTable: "Biblioteke",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
