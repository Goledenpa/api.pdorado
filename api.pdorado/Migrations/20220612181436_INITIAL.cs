using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class INITIAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coleccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEditor = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coleccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coleccion_Editor_IdEditor",
                        column: x => x.IdEditor,
                        principalTable: "Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Lenguaje",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdLenguaje = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Lenguaje", x => new { x.IdEstado, x.IdLenguaje });
                    table.ForeignKey(
                        name: "FK_Estado_Lenguaje_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genero_Lenguaje",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    IdLenguaje = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero_Lenguaje", x => new { x.IdGenero, x.IdLenguaje });
                    table.ForeignKey(
                        name: "FK_Genero_Lenguaje_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paginas = table.Column<int>(type: "int", nullable: false),
                    Existencias = table.Column<int>(type: "int", nullable: false),
                    IdColeccion = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    IdAutor = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comic_Autor_IdAutor",
                        column: x => x.IdAutor,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comic_Coleccion_IdColeccion",
                        column: x => x.IdColeccion,
                        principalTable: "Coleccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comic_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comic_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comic_Lenguaje",
                columns: table => new
                {
                    IdComic = table.Column<int>(type: "int", nullable: false),
                    IdLenguaje = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comic_Lenguaje", x => new { x.IdComic, x.IdLenguaje });
                    table.ForeignKey(
                        name: "FK_Comic_Lenguaje_Comic_IdComic",
                        column: x => x.IdComic,
                        principalTable: "Comic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coleccion_IdEditor",
                table: "Coleccion",
                column: "IdEditor");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_IdAutor",
                table: "Comic",
                column: "IdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_IdColeccion",
                table: "Comic",
                column: "IdColeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_IdEstado",
                table: "Comic",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_IdGenero",
                table: "Comic",
                column: "IdGenero");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comic_Lenguaje");

            migrationBuilder.DropTable(
                name: "Estado_Lenguaje");

            migrationBuilder.DropTable(
                name: "Genero_Lenguaje");

            migrationBuilder.DropTable(
                name: "Comic");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Coleccion");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Editor");
        }
    }
}
