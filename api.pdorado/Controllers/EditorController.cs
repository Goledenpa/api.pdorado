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
    public class EditorController : ControllerBase
    {
        private readonly IDataService<EditorDTO, Editor> _editorService;

        public EditorController(IDataService<EditorDTO, Editor> editorService)
        {
            _editorService = editorService;
        }

        /// <summary>
        /// Obtiene todos los editores
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los editores o un error 404 si no puede obtener los editores</returns>
        [Authorize]
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<EditorDTO>>> GetEditores(int idLenguaje)
        {
            List<EditorDTO> dtos = await _editorService.GetAll(idLenguaje);

            if (dtos == null)
            {
                return NotFound();
            }

            return dtos;
        }

        /// <summary>
        /// Obtiene un editor
        /// </summary>
        /// <param name="id">Id del editor</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El editor o un error 404 si no lo encuentra</returns>
        [Authorize]
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<EditorDTO>> GetEditor(int id, int idLenguaje)
        {
            EditorDTO dto = await _editorService.Get(id, idLenguaje);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }


        /// <summary>
        /// Actualiza un editor
        /// </summary>
        /// <param name="id">Id del editor</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del editor</param>
        /// <returns>El editor actualizado</returns>
        [Authorize]
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<EditorDTO>> UpdateEditor(int id, int idLenguaje, EditorDTO editorDTO)
        {
            EditorDTO dto = await _editorService.Update(id, idLenguaje, editorDTO);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Crea un editor
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="autorDTO">Datos del editor</param>
        /// <returns>El editor creado</returns>
        [Authorize]
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<EditorDTO>> CreateEditor(int idLenguaje, EditorDTO editorDTO)
        {
            EditorDTO dto = await _editorService.Create(idLenguaje, editorDTO);

            if (dto == null)
            {
                return Problem("Entity set 'Comic' is null");
            }

            return CreatedAtAction(nameof(GetEditor), new { id = dto.Id, idLenguaje = idLenguaje }, dto);
        }

        /// <summary>
        /// Elimina un editor
        /// </summary>
        /// <param name="id">El id del editor</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditor(int id)
        {
            bool deleted = await _editorService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
