using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class ColeccionService : IDataService<ColeccionDTO, Coleccion>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ColeccionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Coleccion> ConvertDB(ColeccionDTO dto)
        {
            Coleccion db = _mapper.Map<Coleccion>(dto);

            var comics = new List<Comic>();

            foreach (int idComic in dto.ComicIds)
            {
                Comic comicDB = await _context.Comic.FindAsync(idComic);
                if (comicDB != null)
                {
                    comics.Add(comicDB);
                }
            }

            return db;
        }

        private ColeccionDTO ConvertDTO(Coleccion db)
        {
            ColeccionDTO dto = _mapper.Map<ColeccionDTO>(db);

            dto.ComicIds = db.Comics.Select(x => x.Id).ToList();

            return dto;
        }

        public async Task<ColeccionDTO> Create(int idLenguaje, ColeccionDTO dto)
        {
            if (_context.Coleccion == null)
            {
                return null;
            }

            Coleccion db = await ConvertDB(dto);

            await _context.Coleccion.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            if (_context.Coleccion == null)
            {
                return false;
            }

            Coleccion db = await _context.Coleccion.FindAsync(id);
            if (db == null)
            {
                return false;
            }
            _context.Coleccion.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ColeccionDTO> Get(int id, int idLenguaje)
        {
            if (_context.Coleccion == null)
            {
                return null;
            }
            Coleccion db = await _context.Coleccion.Include(x => x.Comics).FirstOrDefaultAsync(x => x.Id == id);

            if (db == null) 
            {
                return null;
            }

            return ConvertDTO(db);
        }

        public async Task<List<ColeccionDTO>> GetAll(int idLenguaje)
        {
            if (_context.Coleccion == null)
            {
                return null;
            }

            List<Coleccion> dbs = await _context.Coleccion.Include(x => x.Comics).ToListAsync();
            List<ColeccionDTO> dtos = new List<ColeccionDTO>();
            foreach (Coleccion db in dbs)
            {
                dtos.Add(ConvertDTO(db));
            }

            return dtos;

        }

        public async Task<ColeccionDTO> Update(int id, int idLenguaje, ColeccionDTO dto)
        {
            if (_context.Coleccion == null)
            {
                return null;
            }

            if (await _context.Coleccion.FindAsync(id) == null)
            {
                return null;
            }

            Coleccion db = await ConvertDB(dto);

            await _context.SaveChangesAsync();

            return ConvertDTO(db);
        }
    }
}
