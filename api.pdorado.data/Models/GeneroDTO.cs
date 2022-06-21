using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    /// <summary>
    /// DTO de Género
    /// </summary>
    public class GeneroDTO : BaseDTO
    {
        public GeneroDTO()
        {
            ComicIds = new List<int>();
        }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public IList<int> ComicIds { get; set; }
    }
}
