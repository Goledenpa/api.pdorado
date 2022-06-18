using api.pdorado.Configuration;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    /// <summary>
    /// Servicio que se ocupa de todas las operaciones CRUD que se hacen sobre las tabla Estado y Estado_Lenguaje en la base de datos
    /// </summary>
    public class EstadoService : IDataService<EstadoDTO, Estado>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Mapper que ayuda a convertir Estado a EstadoDTO y viceversa
        /// </summary>
        private readonly IMapper _mapper;

        public EstadoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Operaciones CRUD
        /// <summary>
        /// Crea un estado en la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del estado</param>
        /// <returns>DTO del estado que se acaba de crear</returns>
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

        /// <summary>
        /// Elimina un estado de la base de datos
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
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

        /// <summary>
        /// Obtiene un estado de la base de datos
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>Un DTO del estado</returns>
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

        /// <summary>
        /// Obtiene todos los estados de la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los DTOs de los estados</returns>
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

        /// <summary>
        /// Actualiza un estado en la base de datos
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del estado</param>
        /// <returns>El DTO del estado actualizado</returns>
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
        #endregion

        #region Helpers

        /// <summary>
        /// Convierte el objeto de la base de datos a un DTO del estado
        /// </summary>
        /// <param name="db">Objeto de la base de datos</param>
        /// <returns>DTO del estado</returns>
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

        /// <summary>
        /// Convierte el DTO del estado a el objeto de la base de datos
        /// </summary>
        /// <param name="dto">DTO del estado</param>
        /// <returns>Objeto de la base de datos</returns>
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

        /// <summary>
        /// Obtiene el lenguaje del estado
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El lenguaje del estado en el lenguaje especificado</returns>
        private async Task<Estado_Lenguaje?> GetEstadoLenguaje(int id, int idLenguaje)
        {
            return await _context.Estado_Lenguaje.FindAsync(id, idLenguaje);
        }

        /// <summary>
        /// Completa los lenguajes del estado
        /// </summary>
        /// <param name="idComic">Id del estado</param>
        /// <param name="lenguajeExistente">El lenguaje que existe del estado</param>
        /// <returns>Una lista de todos los lenguajes del estado</returns>
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
    }
}
