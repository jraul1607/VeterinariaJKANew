using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CambioImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen2",
                table: "Mascotas",
                type: "varbinary(max)",
                nullable: true);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Imagen2",
                table: "Mascotas");

           
        }
    }
}
