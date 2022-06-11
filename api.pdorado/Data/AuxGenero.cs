using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class AuxGenero
    {
        public AuxGenero()
        {
            AuxGeneroLenguajes = new HashSet<AuxGeneroLenguaje>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual ICollection<AuxGeneroLenguaje> AuxGeneroLenguajes { get; set; }
    }
}
