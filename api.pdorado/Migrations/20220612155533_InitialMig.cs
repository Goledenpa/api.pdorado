using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class InitialMig : Migration
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
                    IdEditor = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Coleccion_Editor_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Lenguaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdLenguaje = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Lenguaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Lenguaje_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genero_Lenguaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    IdLenguaje = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true),
                    ActualizadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EliminadorPor = table.Column<int>(type: "int", nullable: true),
                    EliminadorFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero_Lenguaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genero_Lenguaje_Genero_GeneroId",
                        column: x => x.GeneroId,
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
                    ColeccionId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Comic_Coleccion_ColeccionId",
                        column: x => x.ColeccionId,
                        principalTable: "Coleccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comic_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comic_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutorComic",
                columns: table => new
                {
                    AutoresId = table.Column<int>(type: "int", nullable: false),
                    ComicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorComic", x => new { x.AutoresId, x.ComicsId });
                    table.ForeignKey(
                        name: "FK_AutorComic_Autor_AutoresId",
                        column: x => x.AutoresId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorComic_Comic_ComicsId",
                        column: x => x.ComicsId,
                        principalTable: "Comic",
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
                    ComicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comic_Lenguaje", x => new { x.IdComic, x.IdLenguaje });
                    table.ForeignKey(
                        name: "FK_Comic_Lenguaje_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorComic_ComicsId",
                table: "AutorComic",
                column: "ComicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Coleccion_EditorId",
                table: "Coleccion",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_ColeccionId",
                table: "Comic",
                column: "ColeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_EstadoId",
                table: "Comic",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_GeneroId",
                table: "Comic",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Lenguaje_ComicId",
                table: "Comic_Lenguaje",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_Lenguaje_EstadoId",
                table: "Estado_Lenguaje",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_Lenguaje_GeneroId",
                table: "Genero_Lenguaje",
                column: "GeneroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorComic");

            migrationBuilder.DropTable(
                name: "Comic_Lenguaje");

            migrationBuilder.DropTable(
                name: "Estado_Lenguaje");

            migrationBuilder.DropTable(
                name: "Genero_Lenguaje");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Comic");

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
