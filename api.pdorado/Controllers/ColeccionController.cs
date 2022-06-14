using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using AutoMapper;
using System.Collections.Generic;
using pdorado.data.Models;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColeccionController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public ColeccionController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Coleccion
        [HttpGet("{idLenguaje}")]
        public async Task<ActionResult<IEnumerable<ColeccionDTO>>> GetColecciones(int idLenguaje)
        {
            if (_context.Coleccion == null)
            {
                return NotFound();
            }

            var coleccionesDB = await _context.Coleccion.Include(x => x.Comics).ToListAsync();


            List<ColeccionDTO> coleccionesDTO = new List<ColeccionDTO>();
            foreach (Coleccion coleccion in coleccionesDB)
            {
                object coleccionDTO;

                if ((coleccionDTO = MapColeccion(coleccion)) is ObjectResult)
                {
                    return (ObjectResult)coleccionDTO;
                }

                coleccionesDTO.Add((ColeccionDTO)coleccionDTO);
            }

            return coleccionesDTO;
        }


        // GET: api/Coleccion/5
        [HttpGet("{id}/{idLenguaje}")]
        public async Task<ActionResult<ColeccionDTO>> GetColeccion(int id, int idLenguaje)
        {
            if (_context.Coleccion == null)
            {
                return NotFound();
            }
            var coleccion = await _context.Coleccion.Include(x => x.Comics).FirstOrDefaultAsync(x => x.Id == id);

            if (coleccion == null)
            {
                return NotFound();
            }

            object coleccionDTO;

            if ((coleccionDTO = MapColeccion(coleccion)) is ObjectResult)
            {
                return (ObjectResult)coleccionDTO;
            }

            return (ColeccionDTO)coleccionDTO;
        }

        // PUT: api/Coleccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{idLenguaje}")]
        public async Task<ActionResult<ColeccionDTO>> UpdateColeccion(int id, int idLenguaje, ColeccionDTO coleccionDTO)
        {
            if (id != coleccionDTO.Id)
            {
                return BadRequest();
            }

            Coleccion coleccion;
            if ((coleccion = await MapColeccionDTO(coleccionDTO)) == null)
            {
                return Problem();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ColeccionExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return (ColeccionDTO)MapColeccion(coleccion);
        }

        // POST: api/Coleccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idLenguaje}")]
        public async Task<ActionResult<ColeccionDTO>> CreateColeccion(int idLenguaje, ColeccionDTO coleccionDTO)
        {
            if (_context.Coleccion == null)
            {
                return Problem("Entity set 'DataContext.Coleccion'  is null.");
            }

            Coleccion coleccion = await MapColeccionDTO(coleccionDTO);

            await _context.Coleccion.AddAsync(coleccion);
            await _context.SaveChangesAsync();
            coleccionDTO.Id = coleccion.Id;

            return CreatedAtAction(nameof(GetColeccion), new { id = coleccionDTO.Id }, coleccionDTO);
        }

        // DELETE: api/Coleccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColeccion(int id)
        {
            if (_context.Coleccion == null)
            {
                return NotFound();
            }
            var coleccion = await _context.Coleccion.FindAsync(id);
            if (coleccion == null)
            {
                return NotFound();
            }

            _context.Coleccion.Remove(coleccion);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> ColeccionExist(int id)
        {
            return await (_context.Coleccion.AnyAsync(e => e.Id == id));
        }

        private object MapColeccion(Coleccion coleccion)
        {
            var coleccionDTO = mapper.Map<ColeccionDTO>(coleccion);

            coleccionDTO.ComicIds = coleccion.Comics.Select(e => e.Id).ToList();

            return coleccionDTO;
        }

        private async Task<Coleccion> MapColeccionDTO(ColeccionDTO coleccionDTO)
        {
            Coleccion coleccion = mapper.Map<Coleccion>(coleccionDTO);

            foreach (int idComic in coleccionDTO.ComicIds)
            {
                coleccion.Comics.Add(await _context.Comic.FindAsync(idComic));
            }

            _context.Entry(coleccion).State = EntityState.Modified;

            return coleccion;
        }
    }
}
