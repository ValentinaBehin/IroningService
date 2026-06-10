using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IroningService.Repozitorij.Migrations
{
    /// <inheritdoc />
    public partial class DodajPoljaZaRecenziju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecenzijaKomentar",
                table: "Narudzbe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecenzijaOcjena",
                table: "Narudzbe",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecenzijaKomentar",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "RecenzijaOcjena",
                table: "Narudzbe");
        }
    }
}
