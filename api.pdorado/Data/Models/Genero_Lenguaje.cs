using System.ComponentModel.DataAnnotations.Schema;

namespace api.pdorado.Data.Models
{
    /// <summary>
    /// Modelo sobre el que se va a crear la tabla Genero_Lenguaje en la base de datos
    /// </summary>
    public class Genero_Lenguaje
    {
        public int IdGenero { get; set; }
        public int IdLenguaje { get; set; }
        public string Descripcion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }

        [ForeignKey("IdGenero")]
        public Genero Genero { get; set; }
    }
}
