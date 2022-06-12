using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class Updated_Lenguajes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comic_Lenguaje_Comic_ComicId",
                table: "Comic_Lenguaje");

            migrationBuilder.DropForeignKey(
                name: "FK_Estado_Lenguaje_Estado_EstadoId",
                table: "Estado_Lenguaje");

            migrationBuilder.DropForeignKey(
                name: "FK_Genero_Lenguaje_Genero_GeneroId",
                table: "Genero_Lenguaje");

            migrationBuilder.DropIndex(
                name: "IX_Genero_Lenguaje_GeneroId",
                table: "Genero_Lenguaje");

            migrationBuilder.DropIndex(
                name: "IX_Estado_Lenguaje_EstadoId",
                table: "Estado_Lenguaje");

            migrationBuilder.DropIndex(
                name: "IX_Comic_Lenguaje_ComicId",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Genero_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Estado_Lenguaje");

            migrationBuilder.DropColumn(
                name: "ComicId",
                table: "Comic_Lenguaje");

            migrationBuilder.AddForeignKey(
                name: "FK_Comic_Lenguaje_Comic_IdComic",
                table: "Comic_Lenguaje",
                column: "IdComic",
                principalTable: "Comic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estado_Lenguaje_Estado_IdEstado",
                table: "Estado_Lenguaje",
                column: "IdEstado",
                principalTable: "Estado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genero_Lenguaje_Genero_IdGenero",
                table: "Genero_Lenguaje",
                column: "IdGenero",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comic_Lenguaje_Comic_IdComic",
                table: "Comic_Lenguaje");

            migrationBuilder.DropForeignKey(
                name: "FK_Estado_Lenguaje_Estado_IdEstado",
                table: "Estado_Lenguaje");

            migrationBuilder.DropForeignKey(
                name: "FK_Genero_Lenguaje_Genero_IdGenero",
                table: "Genero_Lenguaje");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Genero_Lenguaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Estado_Lenguaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComicId",
                table: "Comic_Lenguaje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genero_Lenguaje_GeneroId",
                table: "Genero_Lenguaje",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_Lenguaje_EstadoId",
                table: "Estado_Lenguaje",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Lenguaje_ComicId",
                table: "Comic_Lenguaje",
                column: "ComicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comic_Lenguaje_Comic_ComicId",
                table: "Comic_Lenguaje",
                column: "ComicId",
                principalTable: "Comic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estado_Lenguaje_Estado_EstadoId",
                table: "Estado_Lenguaje",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genero_Lenguaje_Genero_GeneroId",
                table: "Genero_Lenguaje",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id");
        }
    }
}
