namespace api.pdorado.Data.Models
{
    public class Genero : BaseDB
    {
        /// <summary>
        /// Modelo sobre el que se va a crear la tabla Genero en la base de datos
        /// </summary>
        public Genero()
        {
            Comics = new List<Comic>();
            Lenguajes = new List<Genero_Lenguaje>();
        }

        public string Codigo { get; set; }
        public IList<Comic> Comics { get; set; }
        public List<Genero_Lenguaje> Lenguajes { get; set; }
    }
}
