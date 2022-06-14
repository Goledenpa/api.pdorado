using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.data.Models;
using api.pdorado.Data.Models;
using AutoMapper;
using System.Collections.Generic;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public EditorController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Editor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditorDTO>>> GetEditores()
        {
            if (_context.Editor == null)
            {
                return NotFound();
            }

            var editoresDB = await _context.Editor.Include(x => x.Colecciones).ToListAsync();

            List<EditorDTO> editoresDTO = new List<EditorDTO>();
            foreach (Editor editor in editoresDB)
            {
                object editorDTO;

                if ((editorDTO = MapEditor(editor)) is ObjectResult)
                {
                    return (ObjectResult)editorDTO;
                }

                editoresDTO.Add((EditorDTO)editorDTO);
            }

            return editoresDTO;
        }


        // GET: api/Editor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EditorDTO>> GetEditor(int id)
        {
            if (_context.Editor == null)
            {
                return NotFound();
            }
            var editor = await _context.Editor.Include(x => x.Colecciones).FirstOrDefaultAsync(x => x.Id == id);

            if (editor == null)
            {
                return NotFound();
            }

            object editorDTO;

            if ((editorDTO = MapEditor(editor)) is ObjectResult)
            {
                return (ObjectResult)editorDTO;
            }

            return (EditorDTO)editorDTO;
        }

        // PUT: api/Editor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<EditorDTO>> UpdateEditor(int id, EditorDTO editorDTO)
        {
            if (id != editorDTO.Id)
            {
                return BadRequest();
            }

            Editor editor;
            if ((editor = await MapEditorDTO(editorDTO)) == null)
            {
                return Problem();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EditorExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return (EditorDTO)MapEditor(editor);
        }

        // POST: api/Editor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EditorDTO>> CreateEditor(EditorDTO editorDTO)
        {
            if (_context.Editor == null)
            {
                return Problem("Entity set 'DataContext.Editor' is null.");
            }

            Editor editor = await MapEditorDTO(editorDTO);

            await _context.Editor.AddAsync(editor);
            await _context.SaveChangesAsync();
            editorDTO.Id = editor.Id;

            return CreatedAtAction(nameof(GetEditor), new { id = editorDTO.Id }, editorDTO);
        }

        // DELETE: api/Editor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditor(int id)
        {
            if (_context.Editor == null)
            {
                return NotFound();
            }
            var editor = await _context.Editor.FindAsync(id);
            if (editor == null)
            {
                return NotFound();
            }

            _context.Editor.Remove(editor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> EditorExist(int id)
        {
            return await (_context.Editor.AnyAsync(e => e.Id == id));
        }

        private object MapEditor(Editor editor)
        {
            var editorDTO = mapper.Map<EditorDTO>(editor);

            editorDTO.ColeccionIds = editor.Colecciones.Select(e => e.Id).ToList();

            return editorDTO;
        }

        private async Task<Editor> MapEditorDTO(EditorDTO editorDTO)
        {
            Editor editor = mapper.Map<Editor>(editorDTO);

            foreach (int idColeccion in editorDTO.ColeccionIds)
            {
                editor.Colecciones.Add(await _context.Coleccion.FindAsync(idColeccion));
            }

            _context.Entry(editor).State = EntityState.Modified;

            return editor;
        }
    }
}
