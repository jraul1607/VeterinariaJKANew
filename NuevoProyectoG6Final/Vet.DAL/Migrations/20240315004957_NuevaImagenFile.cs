using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NuevaImagenFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NuevaImagen",
                table: "Mascotas");

            migrationBuilder.AddColumn<byte[]>(
                name: "NuevaImagens",
                table: "Mascotas",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NuevaImagens",
                table: "Mascotas");

            migrationBuilder.AddColumn<byte[]>(
                name: "NuevaImagen",
                table: "Mascotas",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
