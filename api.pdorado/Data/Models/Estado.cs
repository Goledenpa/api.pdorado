namespace api.pdorado.Data.Models
{
    public class Estado : BaseDB
    {
        public Estado()
        {
            Comics = new List<Comic>();
            Lenguajes = new List<Estado_Lenguaje>();
        }
        public string Codigo { get; set; }
        public IList<Comic> Comics { get; set; }
        public List<Estado_Lenguaje> Lenguajes { get; set; }
    }
}
