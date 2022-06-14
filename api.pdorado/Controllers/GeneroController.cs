using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using AutoMapper;
using pdorado.data.Models;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public GeneroController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Genero
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> GetGeneros(int idLenguaje)
        {
            if (_context.Genero == null)
            {
                return NotFound();
            }

            var generosDB = await _context.Genero.Include(x => x.Comics).ToListAsync();
            List<GeneroDTO> generosDTO = new List<GeneroDTO>();
            foreach (Genero genero in generosDB)
            {
                object generoDTO;

                if ((generoDTO = MapGenero(genero, idLenguaje)) is ObjectResult)
                {
                    return (ObjectResult)generoDTO;
                }

                generosDTO.Add((GeneroDTO)generoDTO);
            }

            return generosDTO;
        }


        // GET: api/Genero/5
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<GeneroDTO>> GetGenero(int id, int idLenguaje)
        {
            if (_context.Genero == null)
            {
                return NotFound();
            }
            var genero = await _context.Genero.Include(x => x.Comics).FirstOrDefaultAsync(x => x.Id == id);

            if (genero == null)
            {
                return NotFound();
            }

            object generoDTO;

            if ((generoDTO = MapGenero(genero, idLenguaje)) is ObjectResult)
            {
                return (ObjectResult)generoDTO;
            }

            return (GeneroDTO)generoDTO;
        }

        // PUT: api/Genero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<GeneroDTO>> UpdateGenero(int id, int idLenguaje, GeneroDTO generoDTO)
        {
            if (id != generoDTO.Id)
            {
                return BadRequest();
            }

            Genero genero;
            if ((genero = await MapGeneroDTO(generoDTO, idLenguaje)) == null)
            {
                return Problem();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GeneroExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return (GeneroDTO)MapGenero(genero, idLenguaje);
        }

        // POST: api/Genero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> CreateGenero(int idLenguaje, GeneroDTO generoDTO)
        {
            if (_context.Genero == null)
            {
                return Problem("Entity set 'DataContext.Genero'  is null.");
            }

            Genero genero = await MapGeneroDTO(generoDTO, idLenguaje);

            await _context.Genero.AddAsync(genero);
            await _context.SaveChangesAsync();
            generoDTO.Id = genero.Id;

            return CreatedAtAction(nameof(GetGenero), new { id = generoDTO.Id, idLenguaje }, generoDTO);
        }

        // DELETE: api/Genero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            if (_context.Genero == null)
            {
                return NotFound();
            }
            var genero = await _context.Genero.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Genero.Remove(genero);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> GeneroExist(int id)
        {
            return await (_context.Genero.AnyAsync(e => e.Id == id));
        }

        private object MapGenero(Genero genero, int idLenguaje)
        {
            var generoDTO = mapper.Map<GeneroDTO>(genero);

            var generoLenguaje = genero.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            if (generoLenguaje == null)
            {
                generoLenguaje = genero.Lenguajes.FirstOrDefault(x => x.Descripcion != null);
                if (generoLenguaje == null)
                {
                    return Problem($"No se ha encontrado ningún lenguaje para el género {genero.Codigo}");
                }
            }

            generoDTO.ComicIds = genero.Comics.Select(e => e.Id).ToList();

            generoDTO.Descripcion = generoLenguaje.Descripcion;

            return generoDTO;
        }

        private async Task<Genero> MapGeneroDTO(GeneroDTO generoDTO, int idLenguaje)
        {
            Genero genero = mapper.Map<Genero>(generoDTO);

            Genero_Lenguaje? generoLenguaje;

            if ((generoLenguaje = GetGeneroLenguaje(genero.Id, idLenguaje)) != null)
            {
                generoLenguaje.ActualizadoPor = genero.ActualizadoPor;
                generoLenguaje.ActualizadoFecha = genero.ActualizadoFecha;
                generoLenguaje.Descripcion = generoDTO.Descripcion;
                _context.Entry(generoLenguaje).State = EntityState.Modified;
            }
            else
            {
                generoLenguaje = new Genero_Lenguaje()
                {
                    IdGenero = genero.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = (int)(genero.ActualizadoPor == null ? genero.CreadoPor : genero.ActualizadoPor),
                    CreadoFecha = (DateTime)(genero.ActualizadoFecha == null ? genero.CreadoFecha : genero.ActualizadoFecha),
                    Descripcion = generoDTO.Descripcion
                };
                await _context.Genero_Lenguaje.AddAsync(generoLenguaje);
            }

            genero.Lenguajes.Add(generoLenguaje);

            foreach (int idComic in generoDTO.ComicIds)
            {
                genero.Comics.Add(await _context.Comic.FindAsync(idComic));
            }

            return genero;
        }

        private Genero_Lenguaje? GetGeneroLenguaje(int id, int idLenguaje)
        {
            var lenguaje = _context.Genero_Lenguaje.FindAsync(id, idLenguaje).Result;
            return lenguaje;
        }
    }
}
