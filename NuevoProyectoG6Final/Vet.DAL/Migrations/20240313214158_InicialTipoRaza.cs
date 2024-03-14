using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InicialTipoRaza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoMascotas",
                columns: table => new
                {
                    IdTipoMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMascotas", x => x.IdTipoMascota);
                });

            migrationBuilder.CreateTable(
                name: "RazaMascotas",
                columns: table => new
                {
                    IdRazaMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoMascota = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RazaMascotas", x => x.IdRazaMascota);
                    table.ForeignKey(
                        name: "FK_RazaMascotas_TipoMascotas_IdTipoMascota",
                        column: x => x.IdTipoMascota,
                        principalTable: "TipoMascotas",
                        principalColumn: "IdTipoMascota",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RazaMascotas_IdTipoMascota",
                table: "RazaMascotas",
                column: "IdTipoMascota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RazaMascotas");

            migrationBuilder.DropTable(
                name: "TipoMascotas");
        }
    }
}
