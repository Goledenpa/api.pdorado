using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class Comic
    {
        public Comic()
        {
            AutorComics = new HashSet<AutorComic>();
            ComicLenguajes = new HashSet<ComicLenguaje>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public int Numero { get; set; }
        public byte[] Imagen { get; set; } = null!;
        public int Paginas { get; set; }
        public int IdEstado { get; set; }
        public int Existencias { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual AuxEstado IdEstadoNavigation { get; set; } = null!;
        public virtual ICollection<AutorComic> AutorComics { get; set; }
        public virtual ICollection<ComicLenguaje> ComicLenguajes { get; set; }
    }
}
