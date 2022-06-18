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
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IDataService<GeneroDTO, Genero> _generoService;

        public GeneroController(IDataService<GeneroDTO, Genero> generoService)
        {
            _generoService = generoService;
        }

        /// <summary>
        /// Obtiene todos los géneros
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los géneros o un error 404 si no puede obtener los géneros</returns>
        [Authorize]
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> GetGeneros(int idLenguaje)
        {
            List<GeneroDTO> dtos = await _generoService.GetAll(idLenguaje);

            if (dtos == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene un género
        /// </summary>
        /// <param name="id">Id del género</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El género o un error 404 si no encuentra el género</returns>
        [Authorize(Admin = true)]
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<GeneroDTO>> GetGenero(int id, int idLenguaje)
        {
            GeneroDTO dto = await _generoService.Get(id, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Actualiza un género
        /// </summary>
        /// <param name="id">Id del género</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del género actualizado</param>
        /// <returns>El género actulizado</returns>
        [Authorize(Admin = true)]
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<GeneroDTO>> UpdateGenero(int id, int idLenguaje, GeneroDTO generoDTO)
        {
            GeneroDTO dto = await _generoService.Update(id, idLenguaje, generoDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Crea un género
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del género</param>
        /// <returns>El género creado</returns>
        [Authorize(Admin = true)]
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> CreateGenero(int idLenguaje, GeneroDTO generoDTO)
        {
            GeneroDTO dto = await _generoService.Create(idLenguaje, generoDTO);

            if (dto == null)
            {
                return Problem("Entity set 'Comic' is null");
            }

            return CreatedAtAction(nameof(GetGenero), new { id = dto.Id, idLenguaje = idLenguaje }, dto);
        }

        /// <summary>
        /// Elimina un género
        /// </summary>
        /// <param name="id">El id del género</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        [Authorize(Admin = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            bool deleted = await _generoService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
