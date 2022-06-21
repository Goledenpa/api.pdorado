namespace pdorado.data.Models
{
    /// <summary>
    /// DTO de Autor
    /// </summary>
    public class AutorDTO : BaseDTO
    {
        public AutorDTO()
        {
            ComicIds = new List<int>();
        }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string? Foto { get; set; }
        public List<int> ComicIds { get; set; }
    }
}
