using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    /// <summary>
    /// Servicio que se ocupa de todas las operaciones CRUD que se hacen sobre la tabla Coleccion en la base de datos
    /// </summary>
    public class ColeccionService : IDataService<ColeccionDTO, Coleccion>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Mapper que ayuda a convertir Coleccion a ColeccionDTO y viceversa
        /// </summary>
        private readonly IMapper _mapper;

        public ColeccionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        #region Operaciones CRUD
        /// <summary>
        /// Crea una colección en la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO de la colección</param>
        /// <returns>DTO de la colección que se acaba de crear</returns>
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

        /// <summary>
        /// Elimina una colección de la base de datos
        /// </summary>
        /// <param name="id">Id de la colección</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
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

        /// <summary>
        /// Obtiene una colección de la base de datos
        /// </summary>
        /// <param name="id">Id de la colección</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>Un DTO de la colección</returns>
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

        /// <summary>
        /// Obtiene todas las colecciones de la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los DTOs de las autores</returns>
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
        /// <summary>
        /// Actualiza una colección en la base de datos
        /// </summary>
        /// <param name="id">Id de la colección</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO de la colección</param>
        /// <returns>El DTO de la colección actualizada</returns>
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
        #endregion

        #region Helpers
        /// <summary>
        /// Convierte el DTO de la colección a el objeto de la base de datos
        /// </summary>
        /// <param name="dto">DTO de la colección</param>
        /// <returns>Objeto de la base de datos</returns>
        private async Task<Coleccion> ConvertDB(ColeccionDTO dto)
        {
            Coleccion db = _mapper.Map<Coleccion>(dto);

            var comics = new List<Comic>();
            Editor comicEditor = await _context.Editor.FindAsync(db.IdEditor);
            if (comicEditor != null)
            {
                db.Editor = comicEditor;
            }
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

        /// <summary>
        /// Convierte el objeto de la base de datos a un DTO de la colección
        /// </summary>
        /// <param name="db">Objeto de la base de datos</param>
        /// <returns>DTO de la colección</returns>
        private ColeccionDTO ConvertDTO(Coleccion db)
        {
            ColeccionDTO dto = _mapper.Map<ColeccionDTO>(db);

            dto.ComicIds = db.Comics.Select(x => x.Id).ToList();

            return dto;
        }
        #endregion
    }
}
