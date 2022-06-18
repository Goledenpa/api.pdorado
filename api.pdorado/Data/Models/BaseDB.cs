namespace api.pdorado.Data.Models
{
    public class BaseDB
    {
        public int Id { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }
}
