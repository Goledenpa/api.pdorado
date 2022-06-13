using api.pdorado.Configuration;
using api.pdorado.Data.Models;
using api.pdorado.Utils;
using Microsoft.EntityFrameworkCore;
using api.pdorado.data.Models;

namespace api.pdorado.Data
{
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

        public DataContext(DbContextOptions options) : base(options) { }

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

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Encrypter.DecryptStringAES(Sesion.Instance.ConnectionString, Sesion.Instance.PublicKey));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
