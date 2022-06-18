using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using AutoMapper;
using System.Collections.Generic;
using api.pdorado.Configuration;
using pdorado.data.Models;
using api.pdorado.Auth;
using api.pdorado.Servicios;
using api.pdorado.Servicios.Interfaces;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IDataService<AutorDTO, Autor> _autorService;

        public AutorController(IDataService<AutorDTO, Autor> autorService)
        {
            _autorService = autorService;
        }

        /// <summary>
        /// Obtiene todos los autores
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los autores o un error 404 si no puede obtener los autores</returns>
        [Authorize]
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutores(int idLenguaje)
        {
            List<AutorDTO> dtos = await _autorService.GetAll(idLenguaje);

            if (dtos == null)
            {
                return NotFound();
            }

            return dtos;
        }


        /// <summary>
        /// Obtiene un autor en específico
        /// </summary>
        /// <param name="id">Id del autor a obtener</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El autor en específico o un error 404 si no lo encuentra</returns>
        [Authorize]
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<AutorDTO>> GetAutor(int id, int idLenguaje)
        {
            AutorDTO dto = await _autorService.Get(id, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return dto;
        }

        /// <summary>
        /// Actualiza un autor
        /// </summary>
        /// <param name="id">Id del autor a actualizar</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del autor para actualizar</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<AutorDTO>> UpdateAutor(int id, int idLenguaje, AutorDTO autorDTO)
        {
            AutorDTO dto = await _autorService.Update(id, idLenguaje, autorDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return dto;
        }

        /// <summary>
        /// Crea un autor
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del autor para crear</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<AutorDTO>> CreateAutor(int idLenguaje, AutorDTO autorDTO)
        {
            AutorDTO dto = await _autorService.Create(idLenguaje, autorDTO);

            if (dto == null) 
            {
                return Problem("Entity set 'Autor' is null");
            }

            return CreatedAtAction(nameof(GetAutor), new { id = dto.Id, idLenguaje = idLenguaje }, dto);
        }

        // DELETE: api/Autor/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            bool deleted = await _autorService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
