using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class AutorService : IDataService<AutorDTO, Autor>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AutorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Autor> ConvertDB(AutorDTO dto)
        {
            Autor db = _mapper.Map<Autor>(dto);

            var comics = new List<Comic>();

            foreach (int idComic in dto.ComicIds)
            {
                Comic comicDB = await _context.Comic.FindAsync(idComic);
                if (comicDB != null)
                {
                    comics.Add(comicDB);
                }
            }

            //_context.Entry(db).State = EntityState.Modified;

            return db;
        }

        private AutorDTO ConvertDTO(Autor db)
        {
            AutorDTO dto = _mapper.Map<AutorDTO>(db);

            dto.ComicIds = db.Comics.Select(x => x.Id).ToList();
            
            return dto;
        }

        public async Task<AutorDTO> Create(int idLenguaje, AutorDTO dto)
        {
            if (_context.Autor == null)
            {
                return null;
            }

            Autor db = await ConvertDB(dto);

            await _context.Autor.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            if (_context.Autor == null)
            {
                return false;
            }

            Autor db = await _context.Autor.FindAsync(id);
            if (db == null)
            {
                return false;
            }

            _context.Autor.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AutorDTO> Get(int id, int idLenguaje)
        {
            if (_context.Autor == null)
            {
                return null;
            }

            Autor db = await _context.Autor.FindAsync(id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db);
        }

        public async Task<List<AutorDTO>> GetAll(int idLenguaje)
        {
            if (_context.Autor == null)
            {
                return null;
            }

            List<Autor> dbs = await _context.Autor.ToListAsync();
            List<AutorDTO> dtos = new List<AutorDTO>();

            foreach (Autor db in dbs)
            {
                dtos.Add(ConvertDTO(db));
            }

            return dtos;
        }

        public async Task<AutorDTO> Update(int id, int idLenguaje, AutorDTO dto)
        {
            if (_context.Autor == null)
            {
                return null;
            }

            if (await _context.Autor.FindAsync(id)  == null)
            {
                return null;
            }

            Autor db = await ConvertDB(dto);

            await _context.SaveChangesAsync();

            return ConvertDTO(db);
        }
    }
}
