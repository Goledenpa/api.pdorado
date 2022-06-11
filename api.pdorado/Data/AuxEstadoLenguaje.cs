using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class AuxEstadoLenguaje
    {
        public int IdEstado { get; set; }
        public int IdLenguaje { get; set; }
        public string Descripcion { get; set; } = null!;
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }

        public virtual AuxEstado IdEstadoNavigation { get; set; } = null!;
        public virtual AuxLenguaje IdLenguajeNavigation { get; set; } = null!;
    }
}
