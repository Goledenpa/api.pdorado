﻿using api.pdorado.Configuration;
using api.pdorado.Data.Models;
using api.pdorado.Utils;
using Microsoft.EntityFrameworkCore;

namespace api.pdorado.Data
{
    /// <summary>
    /// Clase que contiene toda la información de las tablas de la base de datos
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Coleccion> Coleccion { get; set; }
        public DbSet<Comic> Comic { get; set; }
        public DbSet<Comic_Lenguaje> Comic_Lenguaje { get; set; }
        public DbSet<Editor> Editor { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Estado_Lenguaje> Estado_Lenguaje { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Genero_Lenguaje> Genero_Lenguaje { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Cuando se ejecutan el comando "add-migration", observa tanto este método como los DbSet para ver que cambiar en la base de datos
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic_Lenguaje>().HasKey(x => new { x.IdComic, x.IdLenguaje });
            modelBuilder.Entity<Genero_Lenguaje>().HasKey(x => new { x.IdGenero, x.IdLenguaje });
            modelBuilder.Entity<Estado_Lenguaje>().HasKey(x => new { x.IdEstado, x.IdLenguaje });

            modelBuilder.Entity<Comic>().Navigation(x => x.Lenguajes).AutoInclude();
            modelBuilder.Entity<Comic>().Navigation(x => x.Autor).AutoInclude();
            modelBuilder.Entity<Comic>().Navigation(x => x.Coleccion).AutoInclude();
            modelBuilder.Entity<Comic>().Navigation(x => x.Estado).AutoInclude();
            modelBuilder.Entity<Comic>().Navigation(x => x.Genero).AutoInclude();

            modelBuilder.Entity<Genero>().Navigation(x => x.Lenguajes).AutoInclude();
            modelBuilder.Entity<Estado>().Navigation(x => x.Lenguajes).AutoInclude();

            modelBuilder.Entity<Autor>().Navigation(x => x.Comics).AutoInclude();

            modelBuilder.Entity<Usuario>().HasAlternateKey(x => x.Login).HasName("IX_Login");
            modelBuilder.Entity<Coleccion>().HasAlternateKey(x => x.Codigo).HasName("IX_CodigoColeccion");
            modelBuilder.Entity<Comic>().HasAlternateKey(x => x.Codigo).HasName("IX_CodigoComic");
            modelBuilder.Entity<Estado>().HasAlternateKey(x => x.Codigo).HasName("IX_CodigoEstado");
            modelBuilder.Entity<Genero>().HasAlternateKey(x => x.Codigo).HasName("IX_CodigoGenero");

            modelBuilder.Entity<Coleccion>().Navigation(x => x.Editor).AutoInclude();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Cuando se ejecuta el comando "add-migration", mira este método para saber en que dirección tiene que ejecutar la actualizacion
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(Encrypter.DecryptStringAES(Sesion.Instance.ConnectionString, Sesion.Instance.PublicKey));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
