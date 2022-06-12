namespace api.pdorado.Data.Models
{
    public class Estado : BaseDB
    {
        public string Codigo { get; set; }

        public IList<Comic> Comics { get; set; }
        public IList<Estado_Lenguaje> Lenguajes { get; set; }
    }
}
