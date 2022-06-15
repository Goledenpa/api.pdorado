namespace api.pdorado.Data.Models
{
    public class BaseDB
    {
        public int Id { get; set; }
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }
}
