using Microsoft.EntityFrameworkCore;

namespace api.pdorado.Data.Models
{
    /// <summary>
    /// Modelo sobre el que se va a crear la tabla Usuario en la base de datos
    /// </summary>
    public class Usuario : BaseDB
    {
        public string Login { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdLenguaje { get; set; }
        public bool IsAdmin { get; set; }
    }
}
