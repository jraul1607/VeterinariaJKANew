using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TablasCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    IdMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRazaMascota = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUsuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.IdMascota);
                    table.ForeignKey(
                        name: "FK_Mascotas_RazaMascotas_IdRazaMascota",
                        column: x => x.IdRazaMascota,
                        principalTable: "RazaMascotas",
                        principalColumn: "IdRazaMascota",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mascotas_Usuarios_IdUsuarioCreacion",
                        column: x => x.IdUsuarioCreacion,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    IdMedicamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.IdMedicamento);
                });

            migrationBuilder.CreateTable(
                name: "Padecimientos",
                columns: table => new
                {
                    IdPadecimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Padecimientos", x => x.IdPadecimiento);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    IdVacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVacuna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.IdVacuna);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMascota = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioPrincipal = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioSecundario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoCita = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Citas_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IdMascota",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Usuarios_IdUsuarioPrincipal",
                        column: x => x.IdUsuarioPrincipal,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PadecimientoMascotas",
                columns: table => new
                {
                    IdPadecimientoMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMascota = table.Column<int>(type: "int", nullable: false),
                    IdPadecimiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PadecimientoMascotas", x => x.IdPadecimientoMascota);
                    table.ForeignKey(
                        name: "FK_PadecimientoMascotas_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IdMascota",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PadecimientoMascotas_Padecimientos_IdPadecimiento",
                        column: x => x.IdPadecimiento,
                        principalTable: "Padecimientos",
                        principalColumn: "IdPadecimiento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VacunaMascotas",
                columns: table => new
                {
                    IdVacunaMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVacuna = table.Column<int>(type: "int", nullable: false),
                    IdMascota = table.Column<int>(type: "int", nullable: false),
                    FechaVacuna = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacunaMascotas", x => x.IdVacunaMascota);
                    table.ForeignKey(
                        name: "FK_VacunaMascotas_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IdMascota",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacunaMascotas_Vacunas_IdVacuna",
                        column: x => x.IdVacuna,
                        principalTable: "Vacunas",
                        principalColumn: "IdVacuna",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CitaMedicamentos",
                columns: table => new
                {
                    IdCitaMedicamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdMedicamento = table.Column<int>(type: "int", nullable: false),
                    Dosis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaMedicamentos", x => x.IdCitaMedicamento);
                    table.ForeignKey(
                        name: "FK_CitaMedicamentos_Citas_IdCita",
                        column: x => x.IdCita,
                        principalTable: "Citas",
                        principalColumn: "IdCita",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitaMedicamentos_Medicamentos_IdMedicamento",
                        column: x => x.IdMedicamento,
                        principalTable: "Medicamentos",
                        principalColumn: "IdMedicamento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitaMedicamentos_IdCita",
                table: "CitaMedicamentos",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_CitaMedicamentos_IdMedicamento",
                table: "CitaMedicamentos",
                column: "IdMedicamento");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdMascota",
                table: "Citas",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdUsuarioPrincipal",
                table: "Citas",
                column: "IdUsuarioPrincipal");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_IdRazaMascota",
                table: "Mascotas",
                column: "IdRazaMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_IdUsuarioCreacion",
                table: "Mascotas",
                column: "IdUsuarioCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_PadecimientoMascotas_IdMascota",
                table: "PadecimientoMascotas",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_PadecimientoMascotas_IdPadecimiento",
                table: "PadecimientoMascotas",
                column: "IdPadecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_VacunaMascotas_IdMascota",
                table: "VacunaMascotas",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_VacunaMascotas_IdVacuna",
                table: "VacunaMascotas",
                column: "IdVacuna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitaMedicamentos");

            migrationBuilder.DropTable(
                name: "PadecimientoMascotas");

            migrationBuilder.DropTable(
                name: "VacunaMascotas");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Padecimientos");

            migrationBuilder.DropTable(
                name: "Vacunas");

            migrationBuilder.DropTable(
                name: "Mascotas");
        }
    }
}
