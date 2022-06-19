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
    [Migration("20220619231740_indexes")]
    partial class indexes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEditor")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Codigo")
                        .HasName("IX_CodigoColeccion");

                    b.HasIndex("IdEditor");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Existencias")
                        .HasColumnType("int");

                    b.Property<int>("IdAutor")
                        .HasColumnType("int");

                    b.Property<int>("IdColeccion")
                        .HasColumnType("int");

                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int>("IdGenero")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Paginas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Codigo")
                        .HasName("IX_CodigoComic");

                    b.HasIndex("IdAutor");

                    b.HasIndex("IdColeccion");

                    b.HasIndex("IdEstado");

                    b.HasIndex("IdGenero");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdComic", "IdLenguaje");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Codigo")
                        .HasName("IX_CodigoEstado");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEstado", "IdLenguaje");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Codigo")
                        .HasName("IX_CodigoGenero");

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

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGenero", "IdLenguaje");

                    b.ToTable("Genero_Lenguaje");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualizadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreadoFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdLenguaje")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Login")
                        .HasName("IX_Login");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Coleccion", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Editor", "Editor")
                        .WithMany("Colecciones")
                        .HasForeignKey("IdEditor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editor");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Autor", "Autor")
                        .WithMany("Comics")
                        .HasForeignKey("IdAutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.pdorado.Data.Models.Coleccion", "Coleccion")
                        .WithMany("Comics")
                        .HasForeignKey("IdColeccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.pdorado.Data.Models.Estado", "Estado")
                        .WithMany("Comics")
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.pdorado.Data.Models.Genero", "Genero")
                        .WithMany("Comics")
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Coleccion");

                    b.Navigation("Estado");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Comic_Lenguaje", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Comic", "Comic")
                        .WithMany("Lenguajes")
                        .HasForeignKey("IdComic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Estado_Lenguaje", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Estado", "Estado")
                        .WithMany("Lenguajes")
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Genero_Lenguaje", b =>
                {
                    b.HasOne("api.pdorado.Data.Models.Genero", "Genero")
                        .WithMany("Lenguajes")
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("api.pdorado.Data.Models.Autor", b =>
                {
                    b.Navigation("Comics");
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
