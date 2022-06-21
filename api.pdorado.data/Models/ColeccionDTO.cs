using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    public class ColeccionDTO : BaseDTO
    {
        /// <summary>
        /// DTO de Colección
        /// </summary>
        public ColeccionDTO()
        {
            ComicIds = new List<int>();
        }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdEditor { get; set; }
        public string? NombreEditor { get; set; }
        public IList<int> ComicIds { get; set; }
    }
}
