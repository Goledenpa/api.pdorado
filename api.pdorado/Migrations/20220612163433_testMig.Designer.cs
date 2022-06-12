﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.pdorado.Data;

#nullable disable

namespace api.pdorado.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220612163433_testMig")]
    partial class testMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("api.pdorado.Data.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Coleccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<int>("EditorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<int>("IdEditor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EditorId");

                    b.ToTable("Coleccion");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColeccionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<int>("Existencias")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("IdColeccion")
                        .HasColumnType("int");

                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int>("IdGenero")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Paginas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColeccionId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Comic");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic_Lenguaje", b =>
                {
                    b.Property<int>("IdComic")
                        .HasColumnType("int");

                    b.Property<int>("IdLenguaje")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<int>("ComicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdComic", "IdLenguaje");

                    b.HasIndex("ComicId");

                    b.ToTable("Comic_Lenguaje");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Editor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Editor");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Estado_Lenguaje", b =>
                {
                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int>("IdLenguaje")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.HasKey("IdEstado", "IdLenguaje");

                    b.HasIndex("EstadoId");

                    b.ToTable("Estado_Lenguaje");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Genero_Lenguaje", b =>
                {
                    b.Property<int>("IdGenero")
                        .HasColumnType("int");

                    b.Property<int>("IdLenguaje")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActualizadoPor")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreadoPor")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EliminadorFecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EliminadorPor")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("IdGenero", "IdLenguaje");

                    b.HasIndex("GeneroId");

                    b.ToTable("Genero_Lenguaje");
                });

            modelBuilder.Entity("AutorComic", b =>
                {
                    b.Property<int>("AutoresId")
                        .HasColumnType("int");

                    b.Property<int>("ComicsId")
                        .HasColumnType("int");

                    b.HasKey("AutoresId", "ComicsId");

                    b.HasIndex("ComicsId");

                    b.ToTable("AutorComic");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Coleccion", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Editor", "Editor")
                        .WithMany("Colecciones")
                        .HasForeignKey("EditorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editor");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Coleccion", "Coleccion")
                        .WithMany("Comics")
                        .HasForeignKey("ColeccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.pdorado.Data.Models.Estado", "Estado")
                        .WithMany("Comics")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.pdorado.Data.Models.Genero", "Genero")
                        .WithMany("Comics")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coleccion");

                    b.Navigation("Estado");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic_Lenguaje", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Comic", "Comic")
                        .WithMany("Lenguajes")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Estado_Lenguaje", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Estado", "Estado")
                        .WithMany("Lenguajes")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Genero_Lenguaje", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Genero", "Genero")
                        .WithMany("Lenguajes")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("AutorComic", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Autor", null)
                        .WithMany()
                        .HasForeignKey("AutoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.pdorado.Data.Models.Comic", null)
                        .WithMany()
                        .HasForeignKey("ComicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Coleccion", b =>
                {
                    b.Navigation("Comics");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic", b =>
                {
                    b.Navigation("Lenguajes");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Editor", b =>
                {
                    b.Navigation("Colecciones");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Estado", b =>
                {
                    b.Navigation("Comics");

                    b.Navigation("Lenguajes");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Genero", b =>
                {
                    b.Navigation("Comics");

                    b.Navigation("Lenguajes");
                });
#pragma warning restore 612, 618
        }
    }
}
