using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using AutoMapper;
using pdorado.data.Models;
using api.pdorado.Auth;
using api.pdorado.Servicios.Interfaces;

namespace api.pdorado.Controllers
{
    /// <summary>
    /// Controlador de Estado
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        /// <summary>
        /// Servicio que hace todas las operaciones CRUD con la base de datos
        /// </summary>
        private readonly IDataService<EstadoDTO, Estado> _estadoService;

        public EstadoController(IDataService<EstadoDTO, Estado> estadoService)
        {
            _estadoService = estadoService;
        }

        /// <summary>
        /// Obtiene todos los estados
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los estados o un error 404 si no puede obtener los estados</returns>
        [Authorize]
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> GetEstados(int idLenguaje)
        {
            List<EstadoDTO> dtos = await _estadoService.GetAll(idLenguaje);

            if (dtos == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene un estado
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El estado o un error 404 si no encuentra el estado</returns>
        [Authorize(Admin = true)]
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<EstadoDTO>> GetEstado(int id, int idLenguaje)
        {
            EstadoDTO dto = await _estadoService.Get(id, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Obtiene un estado
        /// </summary>
        /// <param name="code">Código del estado</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El estado o un error 404 si no encuentra el estado</returns>
        [Authorize(Admin = true)]
        [HttpGet("code={code}/{idLenguaje}")]
        public async Task<ActionResult<EstadoDTO>> GetEstado(string code, int idLenguaje)
        {
            EstadoDTO dto = await _estadoService.Get(code, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Actualiza un estado
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del estado actualizado</param>
        /// <returns>El estado actulizado</returns>
        [Authorize(Admin = true)]
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<EstadoDTO>> UpdateEstado(int id, int idLenguaje, EstadoDTO estadoDTO)
        {
            EstadoDTO dto = await _estadoService.Update(id, idLenguaje, estadoDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Crea un estado
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del estado</param>
        /// <returns>El estado creado</returns>
        [Authorize(Admin = true)]
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> CreateEstado(int idLenguaje, EstadoDTO estadoDTO)
        {
            EstadoDTO dto = await _estadoService.Create(idLenguaje, estadoDTO);

            if (dto == null)
            {
                return Problem("Entity set 'Comic' is null");
            }

            return CreatedAtAction(nameof(GetEstado), new { id = dto.Id, idLenguaje = idLenguaje }, dto);
        }

        // DELETE: api/Estado/5
        [Authorize(Admin = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            bool deleted = await _estadoService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
