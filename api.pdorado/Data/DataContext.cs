using api.pdorado.Configuration;
using api.pdorado.Data.Models;
using api.pdorado.Utils;
using Microsoft.EntityFrameworkCore;

namespace api.pdorado.Data
{
    public class DataContext : DbContext
    {
        DbSet<Autor> Autor { get; set; }
        DbSet<Coleccion> Coleccion { get; set; }
        DbSet<Comic> Comic { get; set; }
        DbSet<Comic_Lenguaje> Comic_Lenguaje { get; set; }
        DbSet<Editor> Editor { get; set; }
        DbSet<Estado> Estado { get; set; }
        DbSet<Estado_Lenguaje> Estado_Lenguaje { get; set; }
        DbSet<Genero> Genero { get; set; }
        DbSet<Genero_Lenguaje> Genero_Lenguaje { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic_Lenguaje>().HasKey(x => new { x.IdComic, x.IdLenguaje });
            modelBuilder.Entity<Genero_Lenguaje>().HasKey(x => new { x.IdGenero, x.IdLenguaje });
            modelBuilder.Entity<Estado_Lenguaje>().HasKey(x => new { x.IdEstado, x.IdLenguaje });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Encrypter.DecryptStringAES(Sesion.Instance.ConnectionString, Sesion.Instance.PublicKey));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
