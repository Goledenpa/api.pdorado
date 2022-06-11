using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class AutorComic
    {
        public int IdAutor { get; set; }
        public int IdComic { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual Autor IdAutorNavigation { get; set; } = null!;
        public virtual Comic IdComicNavigation { get; set; } = null!;
    }
}
