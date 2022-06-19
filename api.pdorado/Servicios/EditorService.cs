using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    /// <summary>
    /// Servicio que se ocupa de todas las operaciones CRUD que se hacen sobre la tabla Editor en la base de datos
    /// </summary>
    public class EditorService : IDataService<EditorDTO, Editor>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Mapper que ayuda a convertir Editor a EditorDTO y viceversa
        /// </summary>
        private readonly IMapper _mapper;

        public EditorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Operaciones CRUD
        /// <summary>
        /// Crea un editor en la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del editor</param>
        /// <returns>DTO del editor que se acaba de crear</returns>
        public async Task<EditorDTO> Create(int idLenguaje, EditorDTO dto)
        {
            if (_context.Editor == null)
            {
                return null;
            }

            Editor db = await ConvertDB(dto);

            await _context.Editor.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        /// <summary>
        /// Elimina un editor de la base de datos
        /// </summary>
        /// <param name="id">Id del editor</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        public async Task<bool> Delete(int id)
        {
            if (_context.Editor == null)
            {
                return false;
            }

            Editor db = await _context.Editor.FindAsync(id);
            if (db == null)
            {
                return false;
            }
            _context.Editor.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Obtiene un editor de la base de datos
        /// </summary>
        /// <param name="id">Id del editor</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>Un DTO del editor</returns>
        public async Task<EditorDTO> Get(int id, int idLenguaje)
        {
            if (_context.Editor == null)
            {
                return null;
            }
            Editor db = await _context.Editor.Include(x => x.Colecciones).FirstOrDefaultAsync(x => x.Id == id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db);
        }

        public Task<EditorDTO> Get(string code, int idLenguaje)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todos los editores de la base de datos
        /// </summary>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <returns>La lista de todos los DTOs de los editores</returns>
        public async Task<List<EditorDTO>> GetAll(int idLenguaje)
        {
            if (_context.Editor == null)
            {
                return null;
            }

            List<Editor> dbs = await _context.Editor.Include(x => x.Colecciones).ToListAsync();
            List<EditorDTO> dtos = new List<EditorDTO>();
            foreach (Editor db in dbs)
            {
                dtos.Add(ConvertDTO(db));
            }

            return dtos;
        }

        /// <summary>
        /// Actualiza un editor en la base de datos
        /// </summary>
        /// <param name="id">Id del editor</param>
        /// <param name="idLenguaje">El lenguaje de la aplicación en el momento de llamar a la api</param>
        /// <param name="dto">DTO del editor</param>
        /// <returns>El DTO del editor actualizado</returns>
        public async Task<EditorDTO> Update(int id, int idLenguaje, EditorDTO dto)
        {
            if (_context.Editor == null)
            {
                return null;
            }

            if (await _context.Editor.FindAsync(id) == null)
            {
                return null;
            }

            Editor db = await _context.Editor.FindAsync(id);

            _context.Entry(db).CurrentValues.SetValues(dto);

            db = await ConvertDB(dto);

            await _context.SaveChangesAsync();

            return ConvertDTO(db);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Convierte el DTO del editor a el objeto de la base de datos
        /// </summary>
        /// <param name="dto">DTO del editor</param>
        /// <returns>Objeto de la base de datos</returns>
        private async Task<Editor> ConvertDB(EditorDTO dto)
        {
            Editor db = _mapper.Map<Editor>(dto);

            var colecciones = new List<Coleccion>();

            foreach (int idColeccion in dto.ColeccionIds)
            {
                Coleccion coleccionDB = await _context.Coleccion.FindAsync(idColeccion);
                if (coleccionDB != null)
                {
                    colecciones.Add(coleccionDB);
                }
            }

            return db;
        }

        /// <summary>
        /// Convierte el objeto de la base de datos a un DTO del editor
        /// </summary>
        /// <param name="db">Objeto de la base de datos</param>
        /// <returns>DTO del editor</returns>
        private EditorDTO ConvertDTO(Editor db)
        {
            EditorDTO dto = _mapper.Map<EditorDTO>(db);

            dto.ColeccionIds = db.Colecciones.Select(x => x.Id).ToList();

            return dto;
        }
        #endregion
    }
}
