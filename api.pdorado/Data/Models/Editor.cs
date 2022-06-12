namespace api.pdorado.Data.Models
{
    public class Editor : BaseDB
    {
        public string Nombre { get; set; }

        public IList<Coleccion> Colecciones { get; set; }
    }
}
