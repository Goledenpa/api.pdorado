using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class Autor
    {
        public Autor()
        {
            AutorComics = new HashSet<AutorComic>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual ICollection<AutorComic> AutorComics { get; set; }
    }
}
