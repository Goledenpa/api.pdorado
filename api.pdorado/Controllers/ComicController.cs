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
    public class ComicController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public ComicController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Comic
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<ComicDTO>>> GetComics(int idLenguaje)
        {
            if (_context.Comic == null)
            {
                return NotFound();
            }

            var comicsDB = await _context.Comic.ToListAsync();
            List<ComicDTO> comicsDTO = new List<ComicDTO>();
            foreach (Comic comic in comicsDB)
            {
                object comicDTO;

                if ((comicDTO = MapComic(comic, idLenguaje)) is ObjectResult)
                {
                    return (ObjectResult)comicDTO;
                }

                comicsDTO.Add((ComicDTO)comicDTO);
            }

            return comicsDTO;
        }


        // GET: api/Comic/5
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> GetComic(int id, int idLenguaje)
        {
            if (_context.Comic == null)
            {
                return NotFound();
            }
            var comic = await _context.Comic.FindAsync(id);

            if (comic == null)
            {
                return NotFound();
            }

            object comicDTO;

            if ((comicDTO = MapComic(comic, idLenguaje)) is ObjectResult)
            {
                return (ObjectResult)comicDTO;
            }

            return (ComicDTO)comicDTO;
        }

        // PUT: api/Comic/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> UpdateComic(int id, int idLenguaje, ComicDTO comicDTO)
        {
            if (id != comicDTO.Id)
            {
                return BadRequest();
            }

            Comic comic;
            if ((comic = await MapComicDTO(comicDTO, idLenguaje)) == null)
            {
                return Problem();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ComicExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return (ComicDTO)MapComic(comic, idLenguaje);
        }

        // POST: api/Comic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> CreateComic(int idLenguaje, ComicDTO comicDTO)
        {
            if (_context.Comic == null)
            {
                return Problem("Entity set 'DataContext.Comic'  is null.");
            }

            Comic comic = await MapComicDTO(comicDTO, idLenguaje);

            await _context.Comic.AddAsync(comic);
            await _context.SaveChangesAsync();
            comicDTO.Id = comic.Id;

            return CreatedAtAction(nameof(GetComic), new { id = comicDTO.Id, idLenguaje }, comicDTO);
        }

        // DELETE: api/Comic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            if (_context.Comic == null)
            {
                return NotFound();
            }
            var comic = await _context.Comic.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            _context.Comic.Remove(comic);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> ComicExist(int id)
        {
            return await (_context.Comic.AnyAsync(e => e.Id == id));
        }

        private object MapComic(Comic comic, int idLenguaje)
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

            return comicDTO;
        }

        private async Task<Comic> MapComicDTO(ComicDTO comicDTO, int idLenguaje)
        {
            Comic comic = mapper.Map<Comic>(comicDTO);

            comic.Autor = await _context.Autor.FindAsync(comic.Autor.Id);
            comic.Genero = await _context.Genero.FindAsync(comic.Genero.Id);
            comic.Estado = await _context.Estado.FindAsync(comic.Estado.Id);
            comic.Coleccion = await _context.Coleccion.FindAsync(comic.Coleccion.Id);
            Comic_Lenguaje? comicLenguaje;

            if ((comicLenguaje = GetComicLenguaje(comic.Id, idLenguaje)) != null)
            {
                comicLenguaje.ActualizadoPor = comic.ActualizadoPor;
                comicLenguaje.ActualizadoFecha = comic.ActualizadoFecha;
                comicLenguaje.Titulo = comicDTO.Titulo;
                comicLenguaje.Descripcion = comicDTO.Descripcion;
                _context.Entry(comicLenguaje).State = EntityState.Modified;
            }
            else
            {
                comicLenguaje = new Comic_Lenguaje()
                {
                    IdComic = comic.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = (int)(comic.ActualizadoPor == null ? comic.CreadoPor : comic.ActualizadoPor),
                    CreadoFecha = DateTime.Now,
                    Titulo = comicDTO.Titulo,
                    Descripcion = comicDTO.Descripcion
                };
                await _context.Comic_Lenguaje.AddAsync(comicLenguaje);
            }

            comic.Lenguajes.Add(comicLenguaje);

            return comic;
        }

        private Comic_Lenguaje? GetComicLenguaje(int id, int idLenguaje)
        {
            var lenguaje = _context.Comic_Lenguaje.FindAsync(id, idLenguaje).Result;
            return lenguaje;
        }
    }
}
