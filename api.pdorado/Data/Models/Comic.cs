using System.ComponentModel.DataAnnotations.Schema;

namespace api.pdorado.Data.Models
{
    /// <summary>
    /// Modelo sobre el que se va a crear la tabla Comic en la base de datos
    /// </summary>
    public class Comic : BaseDB
    {
        public Comic()
        {
            Lenguajes = new List<Comic_Lenguaje>();
        }
        public string Codigo { get; set; }
        public int Numero { get; set; }
        public string? Imagen { get; set; }
        public int Paginas { get; set; }
        public int Existencias { get; set; }
        public int IdColeccion { get; set; }
        public int IdEstado { get; set; }
        public int IdGenero { get; set; }
        public int IdAutor { get; set; }

        [ForeignKey("IdColeccion")]
        public Coleccion Coleccion { get; set; }
        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }
        [ForeignKey("IdGenero")]
        public Genero Genero { get; set; }
        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }
        public List<Comic_Lenguaje> Lenguajes { get; set; }
    }
}
