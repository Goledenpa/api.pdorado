using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    /// <summary>
    /// Servicio que permite la autentificación del usuario y las operaciones CRUD sobre la tabla Usuario en la base de datos
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        /// <summary>
        /// Context de la base de datos
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Mapper que ayuda en la conversión de Usuario a UsuarioDTO y viceversa
        /// </summary>
        private readonly IMapper _mapper;

        public UsuarioService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Método que comprueba si los datos de usuario son correctos
        /// </summary>
        /// <param name="user">DTO del usuario</param>
        /// <returns>True si el la contraseña es la correcta, false si no</returns>
        public async Task<bool> Login(UsuarioDTO user)
        {
            if (_context.Usuario == null)
            {
                return false;
            }
            Usuario db = await _context.Usuario.Where(x => x.Login == user.Login).FirstOrDefaultAsync();

            return db != null && db.Contrasena == user.Contrasena;
        }

        /// <summary>
        /// Obtiene un usuario de la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <returns>DTO del usuario</returns>
        public async Task<UsuarioDTO> GetUsuario(string login)
        {
            if (_context.Usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioDTO>(await _context.Usuario.Where(x => x.Login == login).FirstOrDefaultAsync());
        }

        /// <summary>
        /// Obtiene todos los usuarios de la base de datos
        /// </summary>
        /// <returns>La lista de los usuarios</returns>
        public async Task<List<UsuarioDTO>> GetAll()
        {
            if (_context.Usuario == null)
            {
                return null;
            }
            return _mapper.Map<List<UsuarioDTO>>(await _context.Usuario.ToListAsync());
        }

        /// <summary>
        /// Crea un usuario en la base de datos
        /// </summary>
        /// <param name="dto">DTO del usuario</param>
        /// <returns>DTO del usuario recién creado</returns>
        public async Task<UsuarioDTO> CreateUsuario(UsuarioDTO dto)
        {
            if (_context.Usuario == null)
            {
                return null;
            }

            Usuario db = _mapper.Map<Usuario>(dto);

            await _context.Usuario.AddAsync(db);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioDTO>(db);
        }

        /// <summary>
        /// Se actualiza un usuario en la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <param name="dto">DTO del usuario</param>
        /// <returns>DTO del usuario actualizado</returns>
        public async Task<UsuarioDTO> UpdateUsuario(string login, UsuarioDTO dto)
        {
            if (_context.Usuario == null)
            {
                return null;
            }

            Usuario db = _mapper.Map<Usuario>(dto);

            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioDTO>(db);
        }

        /// <summary>
        /// Se elimina un usuario de la base de datos
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <returns>True si se ha eliminado correctamente, false si no</returns>
        public async Task<bool> DeleteUsuario(string login)
        {
            if (_context.Usuario == null)
            {
                return false;
            }

            if (await _context.Usuario.Where(x => x.Login == login).FirstOrDefaultAsync() == null)
            {
                return false;
            }

            Usuario db = await _context.Usuario.Where(x => x.Login == login).FirstOrDefaultAsync();

            _context.Usuario.Remove(db);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
