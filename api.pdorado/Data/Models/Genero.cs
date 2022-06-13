namespace api.pdorado.Data.Models
{
    public class Genero : BaseDB
    {
        public Genero()
        {
            Comics = new List<Comic>();
            Lenguajes = new List<Genero_Lenguaje>();
        }

        public string Codigo { get; set; }
        public IList<Comic> Comics { get; set; }
        public IList<Genero_Lenguaje> Lenguajes { get; set; }
    }
}
