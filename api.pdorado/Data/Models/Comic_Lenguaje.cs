namespace api.pdorado.Data.Models
{
    public class Comic_Lenguaje : BaseDB
    {
        public int IdComic { get; set; }
        public int IdLenguaje { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadorPor { get; set; }
        public DateTime? EliminadorFecha { get; set; }

        public Comic Comic { get; set; }
    }
}
