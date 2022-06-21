using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    /// <summary>
    /// DTO base del que heredarán todos los demás DTOs que se usarán para enviar a la api
    /// </summary>
    public class BaseDTO
    {
        public int Id { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }
}
