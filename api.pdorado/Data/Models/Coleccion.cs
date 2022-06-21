using System.ComponentModel.DataAnnotations.Schema;

namespace api.pdorado.Data.Models
{
    /// <summary>
    /// Modelo sobre el que se va a crear la tabla Coleccion en la base de datos
    /// </summary>
    public class Coleccion : BaseDB
    {
        public Coleccion()
        {
            Comics = new List<Comic>();
        }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdEditor { get; set; }
        [ForeignKey("IdEditor")]
        public Editor Editor { get; set; }
        public IList<Comic> Comics { get; set; }
    }
}
