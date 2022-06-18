using api.pdorado.Configuration;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class GeneroService : IDataService<GeneroDTO, Genero>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GeneroService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Helper

        private GeneroDTO ConvertDTO(Genero db, int idLenguaje)
        {
            GeneroDTO dto = _mapper.Map<GeneroDTO>(db);

            Genero_Lenguaje lenguaje = db.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            if (lenguaje == null)
            {
                return null;
            }

            dto.Descripcion = lenguaje.Descripcion;
            dto.ComicIds = db.Comics.Select(x => x.Id).ToList();

            return dto;
        }

        private async Task<Genero> ConvertDB(GeneroDTO dto, int idLenguaje)
        {
            Genero db = _mapper.Map<Genero>(dto);

            Genero_Lenguaje? generoLenguaje;

            if ((generoLenguaje = await GetGeneroLenguaje(db.Id, idLenguaje)) != null)
            {
                generoLenguaje.ActualizadoPor = db.ActualizadoPor;
                generoLenguaje.ActualizadoFecha = db.ActualizadoFecha;
                generoLenguaje.Descripcion = dto.Descripcion;
                _context.Entry(generoLenguaje).State = EntityState.Modified;
            }
            else
            {
                generoLenguaje = new Genero_Lenguaje
                {
                    IdGenero = db.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = db.CreadoPor,
                    CreadoFecha = db.CreadoFecha,
                    Descripcion = dto.Descripcion
                };
                List<Genero_Lenguaje> lenguajes = await CompletarLenguajes(db.Id, generoLenguaje);
                db.Lenguajes.AddRange(lenguajes);
                await _context.Genero_Lenguaje.AddRangeAsync(lenguajes);
            }

            return db;
        }

        private async Task<Genero_Lenguaje?> GetGeneroLenguaje(int id, int idLenguaje)
        {
            return await _context.Genero_Lenguaje.FindAsync(id, idLenguaje);
        }

        private async Task<List<Genero_Lenguaje>> CompletarLenguajes(int idGenero, Genero_Lenguaje lenguajeExistente)
        {
            List<Genero_Lenguaje> lenguajes = new List<Genero_Lenguaje>
            {
                lenguajeExistente
            };

            foreach (int idioma in Sesion.Instance.Idiomas)
            {
                string idiomaTag = Sesion.GetIdiomaTag(idioma);

                var lenguaje = lenguajes.Where(x => x.IdLenguaje == idioma).FirstOrDefault();
                if (lenguaje == null)
                {
                    lenguajes.Add(new Genero_Lenguaje
                    {
                        IdGenero = idGenero,
                        IdLenguaje = idioma,
                        CreadoPor = lenguajeExistente.CreadoPor,
                        CreadoFecha = lenguajeExistente.CreadoFecha,
                        Descripcion = $"{idiomaTag} - {lenguajeExistente.Descripcion}"
                    });
                }
            }

            return lenguajes;
        }

        #endregion
        public async Task<GeneroDTO> Create(int idLenguaje, GeneroDTO dto)
        {
            if (_context.Genero == null)
            {
                return null;
            }

            Genero db = await ConvertDB(dto, idLenguaje);

            await _context.Genero.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            if (_context.Genero == null)
            {
                return false;
            }

            Genero db = await _context.Genero.FindAsync(id);
            if (db == null)
            {
                return false;
            }

            _context.Genero.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<GeneroDTO> Get(int id, int idLenguaje)
        {
            if (_context.Genero == null)
            {
                return null;
            }

            Genero db = await _context.Genero.Include(x => x.Comics).FirstOrDefaultAsync(x => x.Id == id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db, idLenguaje);
        }

        public async Task<List<GeneroDTO>> GetAll(int idLenguaje)
        {
            if (_context.Genero == null)
            {
                return null;
            }

            List<Genero> dbs = await _context.Genero.Include(x => x.Comics).ToListAsync();
            List<GeneroDTO> dtos = new List<GeneroDTO>();
            foreach (Genero db in dbs)
            {
                dtos.Add(ConvertDTO(db, idLenguaje));
            }

            return dtos;
        }

        public async Task<GeneroDTO> Update(int id, int idLenguaje, GeneroDTO dto)
        {
            if (_context.Genero == null)
            {
                return null;
            }

            if (await _context.Genero.FindAsync(id) == null)
            {
                return null;
            }

            Genero db = await ConvertDB(dto, idLenguaje);

            await _context.SaveChangesAsync();

            return ConvertDTO(db, idLenguaje);
        }
    }
}
