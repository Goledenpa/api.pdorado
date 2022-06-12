using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.data.Models;
using api.pdorado.Data.Models;
using AutoMapper;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicDTOesController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public ComicDTOesController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/ComicDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicDTO>>> GetComicDTO(int idLenguaje)
        {
            if (_context.Comic == null)
            {
                return NotFound();
            }

            var comicsDB = await _context.Comic.ToListAsync();
            List<ComicDTO> comicsDTO = new List<ComicDTO>();
            foreach (Comic comic in comicsDB)
            {
                var comicDTO = mapper.Map<ComicDTO>(comic);

                var comicLenguaje = comic.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
                if (comicLenguaje == null)
                {
                    comicLenguaje = comic.Lenguajes.FirstOrDefault(x => x.Titulo != null && x.Descripcion != null);
                    if (comicLenguaje == null)
                    {
                        return Problem($"No se ha encontrado ningún lenguaje para el cómic {comic.Codigo}");
                    }
                }

                comicDTO.Titulo = comicLenguaje.Titulo;
                comicDTO.Descripcion = comicLenguaje.Descripcion;

                var generoLenguaje = comic.Genero.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
                if (generoLenguaje == null)
                {
                    generoLenguaje = comic.Genero.Lenguajes.FirstOrDefault(x => x.Descripcion != null);
                    if (generoLenguaje == null)
                    {
                        return Problem($"No se ha encontrado ningún lenguaje para el género {comic.Genero.Codigo}");
                    }
                }

                comicDTO.NombreGenero = generoLenguaje.Descripcion;

                var estadoLenguaje = comic.Estado.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
                if (estadoLenguaje == null)
                {
                    estadoLenguaje = comic.Estado.Lenguajes.FirstOrDefault(x => x.Descripcion != null);
                    if (estadoLenguaje == null)
                    {
                        return Problem($"No se ha encontrado ningún lenguaje para el estado {comic.Estado.Codigo}");
                    }
                }

                comicDTO.NombreEstado = estadoLenguaje.Descripcion;

                comicsDTO.Add(comicDTO);
            }

            return comicsDTO;
        }
        /*
        // GET: api/ComicDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicDTO>> GetComicDTO(int id, int idLenguaje)
        {
            if (_context.ComicDTO == null)
            {
                return NotFound();
            }
            var comicDTO = await _context.ComicDTO.FindAsync(id);

            if (comicDTO == null)
            {
                return NotFound();
            }

            return comicDTO;
        }

        // PUT: api/ComicDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComicDTO(int id, ComicDTO comicDTO)
        {
            if (id != comicDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(comicDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ComicDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ComicDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComicDTO>> PostComicDTO(ComicDTO comicDTO)
        {
            if (_context.ComicDTO == null)
            {
                return Problem("Entity set 'DataContext.ComicDTO'  is null.");
            }
            await _context.ComicDTO.AddAsync(comicDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComicDTO", new { id = comicDTO.Id }, comicDTO);
        }

        // DELETE: api/ComicDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComicDTO(int id)
        {
            if (_context.ComicDTO == null)
            {
                return NotFound();
            }
            var comicDTO = await _context.ComicDTO.FindAsync(id);
            if (comicDTO == null)
            {
                return NotFound();
            }

            _context.ComicDTO.Remove(comicDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ComicDTOExists(int id)
        {
            return await (_context.ComicDTO?.AnyAsync(e => e.Id == id));
        }*/
    }
}
