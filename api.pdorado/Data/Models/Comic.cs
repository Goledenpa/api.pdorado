namespace api.pdorado.Data.Models
{
    public class Comic : BaseDB
    {
        public string Codigo { get; set; }
        public int Numero { get; set; }
        public string Imagen { get; set; }
        public int Paginas { get; set; }
        public int Existencias { get; set; }
        public int IdColeccion { get; set; }
        public int IdEstado { get; set; }
        public int IdGenero { get; set; }

        public Coleccion Coleccion { get; set; }
        public Estado Estado { get; set; }
        public Genero Genero { get; set; }
        public IList<Comic_Lenguaje> Lenguajes { get; set; }
        public IList<Autor> Autores { get; set; }
    }
}
