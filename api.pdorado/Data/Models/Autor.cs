namespace api.pdorado.Data.Models
{
    public class Autor : BaseDB
    {
        public Autor()
        {
            Comics = new List<Comic>();
        }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public IList<Comic> Comics { get; set; }
    }
}
