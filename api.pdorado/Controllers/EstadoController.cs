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
    public class EstadoController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public EstadoController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Estado
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> GetEstados(int idLenguaje)
        {
            if (_context.Estado == null)
            {
                return NotFound();
            }

            var estadosDB = await _context.Estado.Include(x => x.Comics).ToListAsync();
            List<EstadoDTO> estadosDTO = new List<EstadoDTO>();
            foreach (Estado estado in estadosDB)
            {
                object estadoDTO;

                if ((estadoDTO = MapEstado(estado, idLenguaje)) is ObjectResult)
                {
                    return (ObjectResult)estadoDTO;
                }

                estadosDTO.Add((EstadoDTO)estadoDTO);
            }

            return estadosDTO;
        }


        // GET: api/Estado/5
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<EstadoDTO>> GetEstado(int id, int idLenguaje)
        {
            if (_context.Estado == null)
            {
                return NotFound();
            }
            var estado = await _context.Estado.Include(x => x.Comics).FirstOrDefaultAsync(x => x.Id == id);

            if (estado == null)
            {
                return NotFound();
            }

            object estadoDTO;

            if ((estadoDTO = MapEstado(estado, idLenguaje)) is ObjectResult)
            {
                return (ObjectResult)estadoDTO;
            }

            return (EstadoDTO)estadoDTO;
        }

        // PUT: api/Estado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<EstadoDTO>> UpdateEstado(int id, int idLenguaje, EstadoDTO estadoDTO)
        {
            if (id != estadoDTO.Id)
            {
                return BadRequest();
            }

            Estado estado;
            if ((estado = await MapEstadoDTO(estadoDTO, idLenguaje)) == null)
            {
                return Problem();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EstadoExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return (EstadoDTO)MapEstado(estado, idLenguaje);
        }

        // POST: api/Estado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ComicDTO>> CreateEstado(int idLenguaje, EstadoDTO estadoDTO)
        {
            if (_context.Estado == null)
            {
                return Problem("Entity set 'DataContext.Estado'  is null.");
            }

            Estado estado = await MapEstadoDTO(estadoDTO, idLenguaje);

            await _context.Estado.AddAsync(estado);
            await _context.SaveChangesAsync();
            estadoDTO.Id = estado.Id;

            return CreatedAtAction(nameof(GetEstado), new { id = estadoDTO.Id, idLenguaje }, estadoDTO);
        }

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            if (_context.Estado == null)
            {
                return NotFound();
            }
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> EstadoExist(int id)
        {
            return await (_context.Estado.AnyAsync(e => e.Id == id));
        }

        private object MapEstado(Estado estado, int idLenguaje)
        {
            var estadoDTO = mapper.Map<EstadoDTO>(estado);

            var estadoLenguaje = estado.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            if (estadoLenguaje == null)
            {
                estadoLenguaje = estado.Lenguajes.FirstOrDefault(x => x.Descripcion != null);
                if (estadoLenguaje == null)
                {
                    return Problem($"No se ha encontrado ningún lenguaje para el estado {estado.Codigo}");
                }
            }

            estadoDTO.ComicIds = estado.Comics.Select(e => e.Id).ToList();

            estadoDTO.Descripcion = estadoLenguaje.Descripcion;

            return estadoDTO;
        }

        private async Task<Estado> MapEstadoDTO(EstadoDTO estadoDTO, int idLenguaje)
        {
            Estado estado = mapper.Map<Estado>(estadoDTO);

            Estado_Lenguaje? estadoLenguaje;

            if ((estadoLenguaje = GetEstadoLenguaje(estado.Id, idLenguaje)) != null)
            {
                estadoLenguaje.ActualizadoPor = estado.ActualizadoPor;
                estadoLenguaje.ActualizadoFecha = estado.ActualizadoFecha;
                estadoLenguaje.Descripcion = estadoDTO.Descripcion;
                _context.Entry(estadoLenguaje).State = EntityState.Modified;
            }
            else
            {
                estadoLenguaje = new Estado_Lenguaje()
                {
                    IdEstado = estado.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = (int)(estado.ActualizadoPor == null ? estado.CreadoPor : estado.ActualizadoPor),
                    CreadoFecha = (DateTime)(estado.ActualizadoFecha == null ? estado.CreadoFecha : estado.ActualizadoFecha),
                    Descripcion = estadoDTO.Descripcion
                };
                await _context.Estado_Lenguaje.AddAsync(estadoLenguaje);
            }

            estado.Lenguajes.Add(estadoLenguaje);

            foreach (int comicId in estadoDTO.ComicIds)
            {
                estado.Comics.Add(await _context.Comic.FindAsync(comicId));
            }

            return estado;
        }

        private Estado_Lenguaje? GetEstadoLenguaje(int id, int idLenguaje)
        {
            var lenguaje = _context.Estado_Lenguaje.FindAsync(id, idLenguaje).Result;
            return lenguaje;
        }
    }
}
