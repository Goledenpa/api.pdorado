﻿using Microsoft.AspNetCore.Mvc;
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
    public class AutorController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public AutorController(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Autor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutores()
        {
            if (_context.Autor == null)
            {
                return NotFound();
            }

            var autorDB = await _context.Autor.ToListAsync();
            List<AutorDTO> autoresDTO = new List<AutorDTO>();
            foreach (Autor autor in autorDB)
            {
                object autorDTO;

                if ((autorDTO = MapAutor(autor)) is ObjectResult)
                {
                    return (ObjectResult)autorDTO;
                }

                autoresDTO.Add((AutorDTO)autorDTO);
            }

            return autoresDTO;
        }


        // GET: api/Autor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> GetAutor(int id)
        {
            if (_context.Autor == null)
            {
                return NotFound();
            }
            var autor = await _context.Autor.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            object autorDTO;

            if ((autorDTO = MapAutor(autor)) is ObjectResult)
            {
                return (ObjectResult)autorDTO;
            }

            return (AutorDTO)autorDTO;
        }

        // PUT: api/Autor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAutor(int id, AutorDTO autorDTO)
        {
            if (id != autorDTO.Id)
            {
                return BadRequest();
            }

            Autor autor;
            if ((autor = await MapAutorDTO(autorDTO)) != null)
            {
                return Problem();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AutorExist(id))
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

        // POST: api/Autor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AutorDTO>> CreateComic(AutorDTO autorDTO)
        {
            if (_context.Autor == null)
            {
                return Problem("Entity set 'DataContext.Autor'  is null.");
            }

            Autor autor = await MapAutorDTO(autorDTO);

            await _context.Autor.AddAsync(autor);
            await _context.SaveChangesAsync();
            autorDTO.Id = autor.Id;

            return CreatedAtAction(nameof(GetAutor), new { id = autorDTO.Id }, autorDTO);
        }

        // DELETE: api/Autor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            if (_context.Autor == null)
            {
                return NotFound();
            }
            var autor = await _context.Autor.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Autor.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AutorExist(int id)
        {
            return await (_context.Comic.AnyAsync(e => e.Id == id));
        }

        private object MapAutor(Autor autor)
        {
            var autorDTO = mapper.Map<AutorDTO>(autor);

            autorDTO.ComicIds = autor.Comics.Select(e => e.Id).ToList();

            return autorDTO;
        }

        private async Task<Autor> MapAutorDTO(AutorDTO autorDTO)
        {
            Autor autor = mapper.Map<Autor>(autorDTO);

            var comics = new List<Comic>();

            foreach (int idComic in autorDTO.ComicIds)
            {
                comics.Add(await _context.Comic.FindAsync(idComic));
            }

            _context.Entry(autor).State = EntityState.Modified;

            return autor;
        }
    }
}