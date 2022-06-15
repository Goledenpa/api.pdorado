using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.pdorado.Migrations
{
    public partial class Auditoria_Eliminado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Editor");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Editor");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Comic");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Comic");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Coleccion");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Coleccion");

            migrationBuilder.DropColumn(
                name: "EliminadorFecha",
                table: "Autor");

            migrationBuilder.DropColumn(
                name: "EliminadorPor",
                table: "Autor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Genero",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Genero",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Estado",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Estado",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Editor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Editor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Comic",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Comic",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Coleccion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Coleccion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadorFecha",
                table: "Autor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EliminadorPor",
                table: "Autor",
                type: "int",
                nullable: true);
        }
    }
}
