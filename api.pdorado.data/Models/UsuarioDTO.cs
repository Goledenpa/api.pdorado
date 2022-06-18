using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    public class UsuarioDTO
    {
        public string Login { get; set; }
        public string Contrasena { get; set; }
        public string? Email { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int IdLenguaje { get; set; }
        public bool IsAdmin { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }
}
