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
    /// Servicio que se ocupa de todas las operaciones CRUD que se hacen sobre las tabla Comic y Comic_Lenguaje en la base de datos
    /// </summary>
    public class ComicService : IDataService<ComicDTO, Comic>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Mapper que ayuda a convertir Comic a ComicDTO y viceversa
        /// </summary>
        private readonly IMapper _mapper;

        public ComicService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region Operaciones CRUD
        /// <summary>
        /// Crea un cómic en la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del cómic</param>
        /// <returns>DTO del cómic que se acaba de crear</returns>
        public async Task<ComicDTO> Create(int idLenguaje, ComicDTO dto)
        {
            if (_context.Comic == null) 
            {
                return null;
            }

            Comic db = await ConvertDB(dto, idLenguaje);

            await _context.Comic.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        /// <summary>
        /// Elimina un cómic de la base de datos
        /// </summary>
        /// <param name="id">Id del cómic</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        public async Task<bool> Delete(int id)
        {
            if (_context.Comic == null)
            {
                return false;
            }

            Comic db = await _context.Comic.FindAsync(id);
            if (db == null)
            {
                return false;
            }

            _context.Comic.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Obtiene un cómic de la base de datos
        /// </summary>
        /// <param name="id">Id del cómic</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>Un DTO del cómic</returns>
        public async Task<ComicDTO> Get(int id, int idLenguaje)
        {
            if (_context.Comic == null) 
            {
                return null;
            }

            Comic db = await _context.Comic.FindAsync(id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db, idLenguaje);
        }

        public async Task<ComicDTO> Get(string code, int idLenguaje)
        {
            if (_context.Comic == null)
            {
                return null;
            }

            Comic db = await _context.Comic.FirstOrDefaultAsync(x => x.Codigo == code);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db, idLenguaje);
        }

        /// <summary>
        /// Obtiene todos los cómics de la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los DTOs de los cómics</returns>
        public async Task<List<ComicDTO>> GetAll(int idLenguaje)
        {
            if (_context.Comic == null)
            {
                return null;
            }

            List<Comic> dbs = await _context.Comic.ToListAsync();
            List<ComicDTO> dtos = new List<ComicDTO>();
            foreach (Comic db in dbs)
            {
                dtos.Add(ConvertDTO(db, idLenguaje));
            }

            return dtos;
        }

        /// <summary>
        /// Actualiza un cómic en la base de datos
        /// </summary>
        /// <param name="id">Id del cómic</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del cómic</param>
        /// <returns>El DTO del cómic actualizado</returns>
        public async Task<ComicDTO> Update(int id, int idLenguaje, ComicDTO dto)
        {
            if (_context.Comic ==  null)
            {
                return null;
            }

            if (await _context.Comic.FindAsync(id) == null)
            {
                return null;
            }

            Comic db = await _context.Comic.FindAsync(id);

            _context.Entry(db).CurrentValues.SetValues(dto);

            db = await ConvertDB(dto, idLenguaje);

            await _context.SaveChangesAsync();

            return ConvertDTO(db, idLenguaje);
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Convierte el objeto de la base de datos a un DTO del cómic
        /// </summary>
        /// <param name="db">Objeto de la base de datos</param>
        /// <returns>DTO del cómic</returns>
        private ComicDTO ConvertDTO(Comic db, int idLenguaje)
        {
            ComicDTO dto = _mapper.Map<ComicDTO>(db);

            Comic_Lenguaje lenguaje = db.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            Estado_Lenguaje estadoLenguaje = db.Estado.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            Genero_Lenguaje generoLenguaje = db.Genero.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            if (lenguaje == null || estadoLenguaje == null || generoLenguaje == null)
            {
                return null;
            }

            dto.Descripcion = lenguaje.Descripcion;
            dto.Titulo = lenguaje.Titulo;
            dto.NombreEstado = estadoLenguaje.Descripcion;
            dto.NombreGenero = generoLenguaje.Descripcion;

            return dto;
        }

        /// <summary>
        /// Convierte el DTO del cómic a el objeto de la base de datos
        /// </summary>
        /// <param name="dto">DTO del cómic</param>
        /// <returns>Objeto de la base de datos</returns>
        private async Task<Comic> ConvertDB(ComicDTO dto, int idLenguaje)
        {
            Comic db = _mapper.Map<Comic>(dto);

            Autor autorComic = await _context.Autor.FindAsync(dto.IdAutor);
            Coleccion coleccionComic = await _context.Coleccion.FindAsync(dto.IdColeccion);
            Genero generoComic = await _context.Genero.FindAsync(dto.IdGenero);
            Estado estadoComic = await _context.Estado.FindAsync(dto.IdEstado);

            if (autorComic != null)
            {
                db.Autor = autorComic;
            }

            if (coleccionComic != null)
            {
                db.Coleccion = coleccionComic;
            }

            if (generoComic != null)
            {
                db.Genero = generoComic;
            }

            if (estadoComic != null)
            {
                db.Estado = estadoComic;
            }

            Comic_Lenguaje? comicLenguaje;

            if ((comicLenguaje = await GetComicLenguaje(db.Id, idLenguaje)) != null)
            {
                comicLenguaje.ActualizadoPor = db.ActualizadoPor;
                comicLenguaje.ActualizadoFecha = db.ActualizadoFecha;
                comicLenguaje.Titulo = dto.Titulo;
                comicLenguaje.Descripcion = dto.Descripcion;
                _context.Entry(comicLenguaje).State = EntityState.Modified;
            }
            else
            {
                comicLenguaje = new Comic_Lenguaje
                {
                    IdComic = db.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = db.CreadoPor,
                    CreadoFecha = db.CreadoFecha,
                    Titulo = dto.Titulo,
                    Descripcion = dto.Descripcion
                };
                var lenguajes = await CompletarLenguajes(db.Id, comicLenguaje);
                db.Lenguajes.AddRange(lenguajes);
                await _context.Comic_Lenguaje.AddRangeAsync(lenguajes);
            }

            return db;
        }

        /// <summary>
        /// Obtiene el lenguaje del cómic
        /// </summary>
        /// <param name="id">Id del cómic</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>El lenguaje del cómic en el lenguaje especificado</returns>
        private async Task<Comic_Lenguaje?> GetComicLenguaje(int id, int idLenguaje)
        {
            return await _context.Comic_Lenguaje.FindAsync(id, idLenguaje);
        }

        /// <summary>
        /// Completa los lenguajes del cómic
        /// </summary>
        /// <param name="idComic">Id del cómic</param>
        /// <param name="lenguajeExistente">El lenguaje que existe del cómic</param>
        /// <returns>Una lista de todos los lenguajes del cómic</returns>
        private async Task<List<Comic_Lenguaje>> CompletarLenguajes(int idComic, Comic_Lenguaje lenguajeExistente)
        {
            List<Comic_Lenguaje> lenguajes = new List<Comic_Lenguaje>
            {
                lenguajeExistente
            };

            foreach (int idioma in Sesion.Instance.Idiomas)
            {
                string idiomaTag = Sesion.GetIdiomaTag(idioma);

                var lenguaje = lenguajes.Where(x => x.IdLenguaje == idioma).FirstOrDefault();
                if (lenguaje == null)
                {
                    lenguajes.Add(new Comic_Lenguaje
                    {
                        IdComic = idComic,
                        IdLenguaje = idioma,
                        CreadoPor = lenguajeExistente.CreadoPor,
                        CreadoFecha = lenguajeExistente.CreadoFecha,
                        Descripcion = $"{idiomaTag} - {lenguajeExistente.Descripcion}",
                        Titulo = $"{idiomaTag} - {lenguajeExistente.Titulo}"
                    });
                }
            }

            return lenguajes;
        }
        #endregion
    }
}
