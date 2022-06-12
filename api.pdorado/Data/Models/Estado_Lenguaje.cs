using System.ComponentModel.DataAnnotations.Schema;

namespace api.pdorado.Data.Models
{
    public class Estado_Lenguaje
    {
        public int IdEstado { get; set; }
        public int IdLenguaje { get; set; }
        public string Descripcion { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public int? EliminadorPor { get; set; }
        public DateTime? EliminadorFecha { get; set; }
        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }
    }
}
