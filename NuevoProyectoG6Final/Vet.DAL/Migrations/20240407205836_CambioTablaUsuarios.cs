using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CambioTablaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_IdUsuarioPrincipal",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuarios_IdUsuarioCreacion",
                table: "Mascotas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_IdUsuarioCreacion",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdUsuarioPrincipal",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "IdUsuarioCreacion",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "IdUsuarioPrincipal",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "IdUsuarioSecundario",
                table: "Citas");

            migrationBuilder.AddColumn<string>(
                name: "DuenoId",
                table: "Mascotas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacionId",
                table: "Mascotas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DuenoId",
                table: "Citas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VeterinarioPrincipalId",
                table: "Citas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VeterinarioSecundarioId",
                table: "Citas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Dosis",
                table: "CitaMedicamentos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_DuenoId",
                table: "Mascotas",
                column: "DuenoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioCreacionId",
                table: "Mascotas",
                column: "UsuarioCreacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_DuenoId",
                table: "Citas",
                column: "DuenoId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_VeterinarioPrincipalId",
                table: "Citas",
                column: "VeterinarioPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_VeterinarioSecundarioId",
                table: "Citas",
                column: "VeterinarioSecundarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_DuenoId",
                table: "Citas",
                column: "DuenoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_VeterinarioPrincipalId",
                table: "Citas",
                column: "VeterinarioPrincipalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_VeterinarioSecundarioId",
                table: "Citas",
                column: "VeterinarioSecundarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_AspNetUsers_DuenoId",
                table: "Mascotas",
                column: "DuenoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioCreacionId",
                table: "Mascotas",
                column: "UsuarioCreacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_DuenoId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_VeterinarioPrincipalId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_VeterinarioSecundarioId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_AspNetUsers_DuenoId",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_DuenoId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_DuenoId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_VeterinarioPrincipalId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_VeterinarioSecundarioId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "DuenoId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "DuenoId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "VeterinarioPrincipalId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "VeterinarioSecundarioId",
                table: "Citas");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Mascotas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioCreacion",
                table: "Mascotas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioPrincipal",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioSecundario",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaFechaConexion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_IdUsuarioCreacion",
                table: "Mascotas",
                column: "IdUsuarioCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdUsuarioPrincipal",
                table: "Citas",
                column: "IdUsuarioPrincipal");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_IdUsuarioPrincipal",
                table: "Citas",
                column: "IdUsuarioPrincipal",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuarios_IdUsuarioCreacion",
                table: "Mascotas",
                column: "IdUsuarioCreacion",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
