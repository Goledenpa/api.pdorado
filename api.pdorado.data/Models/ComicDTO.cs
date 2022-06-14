using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    public class ComicDTO : BaseDTO
    {
        public string Codigo { get; set; }
        public int Numero { get; set; }
        public string? Imagen { get; set; }
        public int Paginas { get; set; }
        public int Existencias { get; set; }
        public int IdColeccion { get; set; }
        public string NombreColeccion { get; set; }
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public int IdGenero { get; set; }
        public string NombreGenero { get; set; }
        public int IdAutor { get; set; }
        public string NombreAutor { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
      
    }
}
