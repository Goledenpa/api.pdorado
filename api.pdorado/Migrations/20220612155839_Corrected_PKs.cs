using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class Corrected_PKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genero_Lenguaje",
                table: "Genero_Lenguaje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estado_Lenguaje",
                table: "Estado_Lenguaje");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Genero_Lenguaje");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Estado_Lenguaje");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualizadoFecha",
                table: "Comic_Lenguaje",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActualizadoPor",
                table: "Comic_Lenguaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreadoFecha",
                table: "Comic_Lenguaje",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreadoPor",
                table: "Comic_Lenguaje",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Comic_Lenguaje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genero_Lenguaje",
                table: "Genero_Lenguaje",
                columns: new[] { "IdGenero", "IdLenguaje" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estado_Lenguaje",
                table: "Estado_Lenguaje",
                columns: new[] { "IdEstado", "IdLenguaje" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genero_Lenguaje",
                table: "Genero_Lenguaje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estado_Lenguaje",
                table: "Estado_Lenguaje");

            migrationBuilder.DropColumn(
                name: "ActualizadoFecha",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "CreadoFecha",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Comic_Lenguaje");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comic_Lenguaje");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Genero_Lenguaje",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Estado_Lenguaje",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genero_Lenguaje",
                table: "Genero_Lenguaje",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estado_Lenguaje",
                table: "Estado_Lenguaje",
                column: "Id");
        }
    }
}
