using System;
using System.Collections.Generic;

namespace api.pdorado.Data
{
    public partial class Coleccion
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdEditor { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }
    }
}
