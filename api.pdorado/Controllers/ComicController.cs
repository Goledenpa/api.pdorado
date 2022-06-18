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
    public class ComicController : ControllerBase
    {
        private readonly IDataService<ComicDTO, Comic> _comicService;

        public ComicController(IDataService<ComicDTO, Comic> comicService)
        {
            _comicService = comicService;
        }

        /// <summary>
        /// Obtiene todos los cómics
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los comics o un error 404 si no puede obtener los cómics</returns>
        [Authorize]
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<ComicDTO>>> GetComics(int idLenguaje)
        {
            List<ComicDTO> dtos = await _comicService.GetAll(idLenguaje);

            if (dtos == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene un cómic
        /// </summary>
        /// <param name="id">Id del cómic</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El cómic o un error 404 si no encuentra el cómic</returns>
        [Authorize]
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> GetComic(int id, int idLenguaje)
        {
            ComicDTO dto = await _comicService.Get(id, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Actualiza un cómic
        /// </summary>
        /// <param name="id">Id del cómic</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del cómic actualizado</param>
        /// <returns>El cómic actulizado</returns>
        [Authorize]
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> UpdateComic(int id, int idLenguaje, ComicDTO comicDTO)
        {
            ComicDTO dto = await _comicService.Update(id, idLenguaje, comicDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Crea un cómic
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del cómic</param>
        /// <returns>El cómic creado</returns>
        [Authorize]
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> CreateComic(int idLenguaje, ComicDTO comicDTO)
        {
            ComicDTO dto = await _comicService.Create(idLenguaje, comicDTO);

            if (dto == null)
            {
                return Problem("Entity set 'Comic' is null");
            }

            return CreatedAtAction(nameof(GetComic), new { id = dto.Id, idLenguaje = idLenguaje }, dto);
        }

        /// <summary>
        /// Elimina un cómic
        /// </summary>
        /// <param name="id">El id del cómic</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        [Authorize(Admin = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            bool deleted = await _comicService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
