namespace api.pdorado.Data.Models
{
    public class Editor : BaseDB
    {
        public Editor()
        {
            Colecciones = new List<Coleccion>();
        }
        public string Nombre { get; set; }
        public IList<Coleccion> Colecciones { get; set; }
    }
}
