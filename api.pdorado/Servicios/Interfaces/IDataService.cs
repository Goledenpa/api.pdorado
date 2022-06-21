using api.pdorado.Data.Models;
using pdorado.data.Models;

namespace api.pdorado.Servicios.Interfaces
{
    /// <summary>
    /// Interfaz para todos los servicios que conectan con la base de datos
    /// </summary>
    /// <typeparam name="DTO">DTO de <see cref="DB"/></typeparam>
    /// <typeparam name="DB">Clase de la base de datos</typeparam>
    public interface IDataService<DTO, DB> where DTO : BaseDTO
        where DB : BaseDB
    {
        /// <summary>
        /// Obtiene todas las filas de la tabla
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>Una lista de los DTOs de la tabla</returns>
        Task<List<DTO>> GetAll(int idLenguaje);

        /// <summary>
        /// Obtiene una fila de la tabla
        /// </summary>
        /// <param name="id">Id de la fila</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El DTO del objeto</returns>
        Task<DTO> Get(int id, int idLenguaje);

        /// <summary>
        /// Obtiene una fila de la tabla
        /// </summary>
        /// <param name="code">Código de la fila</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El DTO del objeto</returns>
        Task<DTO> Get(string code, int idLenguaje);

        /// <summary>
        /// Actualiza una fila de la tabla
        /// </summary>
        /// <param name="id">Id de la fila</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">El DTO actualizado</param>
        /// <returns>El DTO actualizado</returns>
        Task<DTO> Update(int id, int idLenguaje, DTO dto);

        /// <summary>
        /// Crea una fila en la tabla
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">El DTO a crear</param>
        /// <returns>El DTO creado</returns>
        Task<DTO> Create(int idLenguaje, DTO dto);

        /// <summary>
        /// Elimina una fila de la tabla
        /// </summary>
        /// <param name="id">Id de la fila</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        Task<bool> Delete(int id);
    }
}
