using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Genero",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Estado",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Comic",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Coleccion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_CodigoGenero",
                table: "Genero",
                column: "Codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_CodigoEstado",
                table: "Estado",
                column: "Codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_CodigoComic",
                table: "Comic",
                column: "Codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_CodigoColeccion",
                table: "Coleccion",
                column: "Codigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "IX_CodigoGenero",
                table: "Genero");

            migrationBuilder.DropUniqueConstraint(
                name: "IX_CodigoEstado",
                table: "Estado");

            migrationBuilder.DropUniqueConstraint(
                name: "IX_CodigoComic",
                table: "Comic");

            migrationBuilder.DropUniqueConstraint(
                name: "IX_CodigoColeccion",
                table: "Coleccion");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Genero",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Estado",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Comic",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Coleccion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
