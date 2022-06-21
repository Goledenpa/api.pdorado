namespace api.pdorado.Data.Models
{
    /// <summary>
    /// Clase base de todas las tablas de la base de datos
    /// </summary>
    public abstract class BaseDB
    {
        public int Id { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }
}
