using System.ComponentModel.DataAnnotations.Schema;

namespace api.pdorado.Data.Models
{
    /// <summary>
    /// Modelo sobre el que se va a crear la tabla Comic_Lenguaje en la base de datos
    /// </summary>
    public class Comic_Lenguaje
    {
        public int IdComic { get; set; }
        public int IdLenguaje { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }

        [ForeignKey("IdComic")]
        public Comic Comic { get; set; }
    }
}
