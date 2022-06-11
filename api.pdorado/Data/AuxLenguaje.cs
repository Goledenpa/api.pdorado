using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class AuxLenguaje
    {
        public AuxLenguaje()
        {
            AuxEstadoLenguajes = new HashSet<AuxEstadoLenguaje>();
            AuxGeneroLenguajes = new HashSet<AuxGeneroLenguaje>();
            AuxLenguajeLenguajeIdLenguajeNavigations = new HashSet<AuxLenguajeLenguaje>();
            AuxLenguajeLenguajeIdNavigations = new HashSet<AuxLenguajeLenguaje>();
            ComicLenguajes = new HashSet<ComicLenguaje>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual ICollection<AuxEstadoLenguaje> AuxEstadoLenguajes { get; set; }
        public virtual ICollection<AuxGeneroLenguaje> AuxGeneroLenguajes { get; set; }
        public virtual ICollection<AuxLenguajeLenguaje> AuxLenguajeLenguajeIdLenguajeNavigations { get; set; }
        public virtual ICollection<AuxLenguajeLenguaje> AuxLenguajeLenguajeIdNavigations { get; set; }
        public virtual ICollection<ComicLenguaje> ComicLenguajes { get; set; }
    }
}
