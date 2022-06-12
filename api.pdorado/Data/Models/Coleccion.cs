namespace api.pdorado.Data.Models
{
    public class Coleccion : BaseDB
    {
        public string Codigo { get; set; }
        public int IdEditor { get; set; }
        public Editor Editor { get; set; }
        public IList<Comic> Comics { get; set; }
    }
}
