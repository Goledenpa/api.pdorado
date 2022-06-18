using Microsoft.EntityFrameworkCore;

namespace api.pdorado.Data.Models
{
    public class Usuario : BaseDB
    {
        public string Login { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdLenguaje { get; set; }
    }
}
