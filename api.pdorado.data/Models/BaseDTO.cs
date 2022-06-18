using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }
}
