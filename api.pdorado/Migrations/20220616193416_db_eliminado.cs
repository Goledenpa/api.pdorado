using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class db_eliminado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Genero_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Genero_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Estado_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Estado_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Comic_Lenguaje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Genero_Lenguaje",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Genero_Lenguaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Estado_Lenguaje",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Estado_Lenguaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Comic_Lenguaje",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Comic_Lenguaje",
                type: "int",
                nullable: true);
        }
    }
}
