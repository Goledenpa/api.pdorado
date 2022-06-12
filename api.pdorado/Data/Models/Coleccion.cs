using System.ComponentModel.DataAnnotations.Schema;

namespace api.pdorado.Data.Models
{
    public class Coleccion : BaseDB
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdEditor { get; set; }
        [ForeignKey("IdEditor")]
        public Editor Editor { get; set; }
        public IList<Comic> Comics { get; set; }
    }
}
