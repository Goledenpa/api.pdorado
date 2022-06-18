using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using AutoMapper;
using System.Collections.Generic;
using pdorado.data.Models;
using api.pdorado.Auth;
using api.pdorado.Servicios.Interfaces;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColeccionController : ControllerBase
    {
        private readonly IDataService<ColeccionDTO, Coleccion> _coleccionService;

        public ColeccionController(IDataService<ColeccionDTO, Coleccion> coleccionService)
        {
            _coleccionService = coleccionService;
        }

        /// <summary>
        /// Obtiene todas las colecciones
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todas las colecciones o un error 404 si no puede obtener las colecciones</returns>
        [Authorize]
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<ColeccionDTO>>> GetColecciones(int idLenguaje)
        {
            List<ColeccionDTO> dtos = await _coleccionService.GetAll(idLenguaje);

            if (dtos == null)
            {
                return NotFound();
            }

            return dtos;
        }


        /// <summary>
        /// Obtiene una colección
        /// </summary>
        /// <param name="id">Id de la colección</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La colección o un error 404 si no lo encuentra</returns>
        [Authorize]
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<ColeccionDTO>> GetColeccion(int id, int idLenguaje)
        {
            ColeccionDTO dto = await _coleccionService.Get(id, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Actualiza una colección
        /// </summary>
        /// <param name="id">Id de la colección</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos de la colección</param>
        /// <returns>La colección actualizada</returns>
        [Authorize]
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<ColeccionDTO>> UpdateColeccion(int id, int idLenguaje, ColeccionDTO coleccionDTO)
        {
            ColeccionDTO dto = await _coleccionService.Update(id, idLenguaje, coleccionDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // POST: api/Coleccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ColeccionDTO>> CreateColeccion(int idLenguaje, ColeccionDTO coleccionDTO)
        {
            ColeccionDTO dto = await _coleccionService.Create(idLenguaje, coleccionDTO);

            if (dto == null)
            {
                return Problem("Entity set 'Autor' is null");
            }

            return CreatedAtAction(nameof(GetColeccion), new { id = dto.Id, idLenguaje = idLenguaje }, dto);
        }

        // DELETE: api/Coleccion/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColeccion(int id)
        {
            bool deleted = await _coleccionService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
