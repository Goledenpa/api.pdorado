using api.pdorado.Configuration;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using api.pdorado.Utils;
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

        public async Task<bool> Login(UsuarioDTO user)
        {
            if (_context.Usuario == null)
            {
                return false;
            }
            Usuario db = await _context.Usuario.Where(x => x.Login == user.Login).FirstOrDefaultAsync();
            HashManager hasher = new HashManager();


            return db != null && hasher.Verify(user.Contrasena, db.Contrasena);
        }

        public async Task<UsuarioDTO> GetUsuario(string login)
        {
            if (_context.Usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioDTO>(await _context.Usuario.Where(x => x.Login == login).FirstOrDefaultAsync());
        }
        public async Task<List<UsuarioDTO>> GetAll()
        {
            if (_context.Usuario == null)
            {
                return null;
            }
            return _mapper.Map<List<UsuarioDTO>>(await _context.Usuario.ToListAsync());
        }

        public async Task<UsuarioDTO> CreateUsuario(UsuarioDTO dto)
        {
            HashManager hasher = new HashManager();
            if (_context.Usuario == null)
            {
                return null;
            }

            Usuario db = _mapper.Map<Usuario>(dto);
            db.Contrasena = hasher.HashToString(db.Contrasena);

            await _context.Usuario.AddAsync(db);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioDTO>(db);
        }

        public async Task<UsuarioDTO> UpdateUsuario(int id, UsuarioDTO dto)
        {
            if (_context.Usuario == null)
            {
                return null;
            }

            if (await _context.Usuario.FindAsync(id) == null)
            {
                return null;
            }

            Usuario db = await _context.Usuario.FindAsync(id);

            _context.Entry(db).CurrentValues.SetValues(dto);

            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioDTO>(db);
        }

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
