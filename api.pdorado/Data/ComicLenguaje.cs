using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class ComicLenguaje
    {
        public int IdComic { get; set; }
        public int IdLenguaje { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Sinopsis { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual Comic IdComicNavigation { get; set; } = null!;
        public virtual AuxLenguaje IdLenguajeNavigation { get; set; } = null!;
    }
}
