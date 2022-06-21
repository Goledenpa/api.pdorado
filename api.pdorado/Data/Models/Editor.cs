namespace api.pdorado.Data.Models
{
    public class Editor : BaseDB
    {
        /// <summary>
        /// Modelo sobre el que se va a crear la tabla Editor en la base de datos
        /// </summary>
        public Editor()
        {
            Colecciones = new List<Coleccion>();
        }
        public string Nombre { get; set; }
        public IList<Coleccion> Colecciones { get; set; }
    }
}
