namespace pdorado.data.Models
{
    public class AutorDTO : BaseDTO
    {
        public AutorDTO()
        {
            ComicIds = new List<int>();
        }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public List<int> ComicIds { get; set; }
    }
}
