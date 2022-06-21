using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdorado.data.Models
{
    /// <summary>
    /// DTO de Editor
    /// </summary>
    public class EditorDTO : BaseDTO
    {
        public EditorDTO()
        {
            ColeccionIds = new List<int>();
        }
        public string Nombre { get; set; }
        public IList<int> ColeccionIds { get; set; }
    }
}
