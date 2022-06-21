using pdorado.data.Models;

namespace api.pdorado.Servicios.Interfaces
{
    /// <summary>
    /// Interfaz que define los métodos para interactuar con la tabla Usuario
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Método que comprueba si los datos de usuario son correctos
        /// </summary>
        /// <param name="user">DTO del usuario</param>
        /// <returns>True si el la contraseña es la correcta, false si no</returns>
        Task<bool> Login(UsuarioDTO login);

        /// <summary>
        /// Obtiene un usuario de la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <returns>DTO del usuario</returns>
        Task<UsuarioDTO> GetUsuario(string login);

        /// <summary>
        /// Obtiene todos los usuarios de la base de datos
        /// </summary>
        /// <returns>La lista de los usuarios</returns>
        Task<List<UsuarioDTO>> GetAll();

        /// <summary>
        /// Crea un usuario en la base de datos
        /// </summary>
        /// <param name="dto">DTO del usuario</param>
        /// <returns>DTO del usuario recién creado</returns>
        Task<UsuarioDTO> CreateUsuario(UsuarioDTO dto);

        /// <summary>
        /// Se actualiza un usuario en la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <param name="dto">DTO del usuario</param>
        /// <returns>DTO del usuario actualizado</returns>
        Task<UsuarioDTO> UpdateUsuario(int id, UsuarioDTO dto);


        /// <summary>
        /// Se elimina un usuario de la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        Task<bool> DeleteUsuario(string login);
    }
}
