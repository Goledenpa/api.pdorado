﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.pdorado.data.Models
{
    public class EstadoDTO : BaseDTO
    {
        public EstadoDTO()
        {
            ComicIds = new List<int>();
        }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public IList<int> ComicIds { get; set; }
    }
}
