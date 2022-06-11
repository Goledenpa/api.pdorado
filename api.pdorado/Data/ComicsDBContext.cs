using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.pdorado.Data
{
    public partial class ComicsDBContext : DbContext
    {
        public ComicsDBContext()
        {
        }

        public ComicsDBContext(DbContextOptions<ComicsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<AutorComic> AutorComics { get; set; } = null!;
        public virtual DbSet<AuxEstado> AuxEstados { get; set; } = null!;
        public virtual DbSet<AuxEstadoLenguaje> AuxEstadoLenguajes { get; set; } = null!;
        public virtual DbSet<AuxGenero> AuxGeneros { get; set; } = null!;
        public virtual DbSet<AuxGeneroLenguaje> AuxGeneroLenguajes { get; set; } = null!;
        public virtual DbSet<AuxLenguaje> AuxLenguajes { get; set; } = null!;
        public virtual DbSet<AuxLenguajeLenguaje> AuxLenguajeLenguajes { get; set; } = null!;
        public virtual DbSet<Coleccion> Coleccions { get; set; } = null!;
        public virtual DbSet<Comic> Comics { get; set; } = null!;
        public virtual DbSet<ComicLenguaje> ComicLenguajes { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CS_AS");

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("Autor");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(30);
            });

            modelBuilder.Entity<AutorComic>(entity =>
            {
                entity.HasKey(e => new { e.IdAutor, e.IdComic });

                entity.ToTable("Autor_Comic");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.AutorComics)
                    .HasForeignKey(d => d.IdAutor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autor_Comic_Autor");

                entity.HasOne(d => d.IdComicNavigation)
                    .WithMany(p => p.AutorComics)
                    .HasForeignKey(d => d.IdComic)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autor_Comic_Comic");
            });

            modelBuilder.Entity<AuxEstado>(entity =>
            {
                entity.ToTable("AUX_Estado");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Codigo).HasMaxLength(5);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuxEstadoLenguaje>(entity =>
            {
                entity.HasKey(e => new { e.IdLenguaje, e.IdEstado });

                entity.ToTable("AUX_Estado_Lenguaje");

                entity.Property(e => e.IdLenguaje).ValueGeneratedOnAdd();

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).HasMaxLength(20);

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.AuxEstadoLenguajes)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUX_Estado_Lenguaje_AUX_Estado");

                entity.HasOne(d => d.IdLenguajeNavigation)
                    .WithMany(p => p.AuxEstadoLenguajes)
                    .HasForeignKey(d => d.IdLenguaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUX_Estado_Lenguaje_AUX_Lenguaje");
            });

            modelBuilder.Entity<AuxGenero>(entity =>
            {
                entity.ToTable("AUX_Genero");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Codigo).HasMaxLength(5);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuxGeneroLenguaje>(entity =>
            {
                entity.HasKey(e => new { e.IdGenero, e.IdLenguaje });

                entity.ToTable("AUX_Genero_Lenguaje");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).HasMaxLength(30);

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.AuxGeneroLenguajes)
                    .HasForeignKey(d => d.IdGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUX_Genero_Lenguaje_AUX_Genero");

                entity.HasOne(d => d.IdLenguajeNavigation)
                    .WithMany(p => p.AuxGeneroLenguajes)
                    .HasForeignKey(d => d.IdLenguaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUX_Genero_Lenguaje_AUX_Lenguaje");
            });

            modelBuilder.Entity<AuxLenguaje>(entity =>
            {
                entity.ToTable("AUX_Lenguaje");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Codigo).HasMaxLength(5);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuxLenguajeLenguaje>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.IdLenguaje });

                entity.ToTable("AUX_Lenguaje_Lenguaje");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Descripcion).HasMaxLength(30);

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.AuxLenguajeLenguajeIdNavigations)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUX_Lenguaje_Lenguaje_AUX_Lenguaje1");

                entity.HasOne(d => d.IdLenguajeNavigation)
                    .WithMany(p => p.AuxLenguajeLenguajeIdLenguajeNavigations)
                    .HasForeignKey(d => d.IdLenguaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUX_Lenguaje_Lenguaje_AUX_Lenguaje");
            });

            modelBuilder.Entity<Coleccion>(entity =>
            {
                entity.ToTable("Coleccion");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Codigo).HasMaxLength(10);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Comic>(entity =>
            {
                entity.ToTable("Comic");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Codigo).HasMaxLength(10);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.FechaAdquisicion).HasColumnType("datetime");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Comics)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comic_AUX_Estado");
            });

            modelBuilder.Entity<ComicLenguaje>(entity =>
            {
                entity.HasKey(e => new { e.IdComic, e.IdLenguaje });

                entity.ToTable("Comic_Lenguaje");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Sinopsis).HasMaxLength(500);

                entity.Property(e => e.Titulo).HasMaxLength(50);

                entity.HasOne(d => d.IdComicNavigation)
                    .WithMany(p => p.ComicLenguajes)
                    .HasForeignKey(d => d.IdComic)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comic_Lenguaje_Comic");

                entity.HasOne(d => d.IdLenguajeNavigation)
                    .WithMany(p => p.ComicLenguajes)
                    .HasForeignKey(d => d.IdLenguaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comic_Lenguaje_AUX_Lenguaje");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
