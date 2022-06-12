namespace api.pdorado.Data.Models
{
    public class Genero : BaseDB
    {
        public string Codigo { get; set; }

        public IList<Comic> Comics { get; set; }
        public IList<Genero_Lenguaje> Lenguajes { get; set; }
    }
}
