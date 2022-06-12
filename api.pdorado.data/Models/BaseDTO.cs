using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.pdorado.data.Models
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadorPor { get; set; }
        public DateTime? EliminadorFecha { get; set; }
    }
}
