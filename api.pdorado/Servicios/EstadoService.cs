using api.pdorado.Configuration;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class EstadoService : IDataService<EstadoDTO, Estado>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EstadoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Helper

        private EstadoDTO ConvertDTO(Estado db, int idLenguaje)
        {
            EstadoDTO dto = _mapper.Map<EstadoDTO>(db);

            Estado_Lenguaje lenguaje = db.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            if (lenguaje == null)
            {
                return null;
            }

            dto.Descripcion = lenguaje.Descripcion;
            dto.ComicIds = db.Comics.Select(x => x.Id).ToList();

            return dto;
        }

        private async Task<Estado> ConvertDB(EstadoDTO dto, int idLenguaje)
        {
            Estado db = _mapper.Map<Estado>(dto);

            Estado_Lenguaje? estadoLenguaje;

            if ((estadoLenguaje = await GetEstadoLenguaje(db.Id, idLenguaje)) != null)
            {
                estadoLenguaje.ActualizadoPor = db.ActualizadoPor;
                estadoLenguaje.ActualizadoFecha = db.ActualizadoFecha;
                estadoLenguaje.Descripcion = dto.Descripcion;
                _context.Entry(estadoLenguaje).State = EntityState.Modified;
            }
            else
            {
                estadoLenguaje = new Estado_Lenguaje
                {
                    IdEstado = db.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = db.CreadoPor,
                    CreadoFecha = db.CreadoFecha,
                    Descripcion = dto.Descripcion
                };
                List<Estado_Lenguaje> lenguajes = await CompletarLenguajes(db.Id, estadoLenguaje);
                db.Lenguajes.AddRange(lenguajes);
                await _context.Estado_Lenguaje.AddRangeAsync(lenguajes);
            }

            return db;
        }

        private async Task<Estado_Lenguaje?> GetEstadoLenguaje(int id, int idLenguaje)
        {
            return await _context.Estado_Lenguaje.FindAsync(id, idLenguaje);
        }

        private async Task<List<Estado_Lenguaje>> CompletarLenguajes(int idEstado, Estado_Lenguaje lenguajeExistente)
        {
            List<Estado_Lenguaje> lenguajes = new List<Estado_Lenguaje>
            {
                lenguajeExistente
            };

            foreach (int idioma in Sesion.Instance.Idiomas)
            {
                string idiomaTag = Sesion.GetIdiomaTag(idioma);

                var lenguaje = lenguajes.Where(x => x.IdLenguaje == idioma).FirstOrDefault();
                if (lenguaje == null)
                {
                    lenguajes.Add(new Estado_Lenguaje
                    {
                        IdEstado = idEstado,
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

        public async Task<EstadoDTO> Create(int idLenguaje, EstadoDTO dto)
        {
            if (_context.Estado == null)
            {
                return null;
            }

            Estado db = await ConvertDB(dto, idLenguaje);

            await _context.Estado.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            if (_context.Estado == null)
            {
                return false;
            }

            Estado db = await _context.Estado.FindAsync(id);
            if (db == null)
            {
                return false;
            }

            _context.Estado.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EstadoDTO> Get(int id, int idLenguaje)
        {
            if (_context.Estado == null)
            {
                return null;
            }

            Estado db = await _context.Estado.Include(x => x.Comics).FirstOrDefaultAsync(x => x.Id == id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db, idLenguaje);
        }

        public async Task<List<EstadoDTO>> GetAll(int idLenguaje)
        {
            if (_context.Estado == null)
            {
                return null;
            }

            List<Estado> dbs = await _context.Estado.Include(x => x.Comics).ToListAsync();
            List<EstadoDTO> dtos = new List<EstadoDTO>();
            foreach (Estado db in dbs)
            {
                dtos.Add(ConvertDTO(db, idLenguaje));
            }

            return dtos;
        }

        public async Task<EstadoDTO> Update(int id, int idLenguaje, EstadoDTO dto)
        {
            if (_context.Estado == null)
            {
                return null;
            }

            if (await _context.Estado.FindAsync(id) == null)
            {
                return null;
            }

            Estado db = await ConvertDB(dto, idLenguaje);

            await _context.SaveChangesAsync();

            return ConvertDTO(db, idLenguaje);
        }
    }
}
