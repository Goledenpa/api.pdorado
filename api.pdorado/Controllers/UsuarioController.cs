using api.pdorado.Auth;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using pdorado.data.Models;

namespace api.pdorado.Controllers
{
    /// <summary>
    /// Controlador de los usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Servicio que hace todas las operaciones CRUD en la tabla Usuario
        /// </summary>
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>La lista de todos los usuarios o un error 404 si no puede obtener los usuarios</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            List<UsuarioDTO> dtos = await _usuarioService.GetAll();

            if (dtos == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene un usuario
        /// </summary>
        /// <param name="login">Login del usuario a obtener</param>
        /// <returns>El autor o un error 404 si no lo encuentra</returns>
        [Authorize]
        [HttpGet("{login}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(string login)
        {
            UsuarioDTO dto = await _usuarioService.GetUsuario(login);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <param name="usuarioDTO">Datos del usuario para actualizar</param>
        /// <returns>El usuario actualizado</returns>
        [Authorize]
        [HttpPut("{login}")]
        public async Task<ActionResult<UsuarioDTO>> UpdateUsuario(string login, UsuarioDTO usuarioDTO)
        {
            UsuarioDTO dto = await _usuarioService.UpdateUsuario(login, usuarioDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario para crear</param>
        /// <returns>El usuario creado</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CreateAutor(UsuarioDTO usuarioDTO)
        {
            UsuarioDTO dto = await _usuarioService.CreateUsuario(usuarioDTO);

            if (dto == null)
            {
                return Problem("Entity set 'Usuario' is null");
            }

            return CreatedAtAction(nameof(GetUsuario), new { login = usuarioDTO.Login }, dto);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">El id del usuario</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        [Authorize]
        [HttpDelete("{login}")]
        public async Task<IActionResult> DeleteUsuario(string login)
        {
            bool deleted = await _usuarioService.DeleteUsuario(login);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
