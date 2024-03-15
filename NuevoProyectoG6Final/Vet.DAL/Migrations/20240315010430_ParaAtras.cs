using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ParaAtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NuevaImagens",
                table: "Mascotas");

            migrationBuilder.RenameColumn(
                name: "imagen",
                table: "Mascotas",
                newName: "Imagen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagen",
                table: "Mascotas",
                newName: "imagen");

            migrationBuilder.AddColumn<byte[]>(
                name: "NuevaImagens",
                table: "Mascotas",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
