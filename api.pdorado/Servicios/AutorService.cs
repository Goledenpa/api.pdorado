using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    /// <summary>
    /// Servicio que se ocupa de todas las operaciones CRUD que se hacen sobre la tabla Autor en la base de datos
    /// </summary>
    public class AutorService : IDataService<AutorDTO, Autor>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Mapper que ayuda a convertir Autor a AutorDTO y viceversa
        /// </summary>
        private readonly IMapper _mapper;

        public AutorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Operaciones CRUD
        /// <summary>
        /// Crea un autor en la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del autor</param>
        /// <returns>DTO del autor que se acaba de crear</returns>
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

        /// <summary>
        /// Elimina un autor de la base de datos
        /// </summary>
        /// <param name="id">Id del autor</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
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

        /// <summary>
        /// Obtiene un autor de la base de datos
        /// </summary>
        /// <param name="id">Id del autor</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>Un DTO del autor</returns>
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

        /// <summary>
        /// Obtiene todos los autores de la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los DTOs de los autores</returns>
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

        /// <summary>
        /// Actualiza un autor en la base de datos
        /// </summary>
        /// <param name="id">Id del autor</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del autor</param>
        /// <returns>El DTO del autor actualizado</returns>
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
        #endregion

        #region Helpers
        /// <summary>
        /// Convierte el DTO del autor a el objeto de la base de datos
        /// </summary>
        /// <param name="dto">DTO del autor</param>
        /// <returns>Objeto de la base de datos</returns>
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

            return db;
        }

        /// <summary>
        /// Convierte el objeto de la base de datos a un DTO del autor
        /// </summary>
        /// <param name="db">Objeto de la base de datos</param>
        /// <returns>DTO del autor</returns>
        private AutorDTO ConvertDTO(Autor db)
        {
            AutorDTO dto = _mapper.Map<AutorDTO>(db);

            dto.ComicIds = db.Comics.Select(x => x.Id).ToList();

            return dto;
        }

        #endregion
    }
}
