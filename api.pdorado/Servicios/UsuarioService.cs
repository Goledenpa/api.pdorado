using api.pdorado.Data;
using AutoMapper;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool Login(UsuarioDTO user)
        {
            var userDB = _context.Usuario.Where(x => x.Login == user.Login).FirstOrDefault();

            return userDB != null && userDB.Contrasena == user.Contrasena;
        }

        public UsuarioDTO GetUsuario(string login)
        {
            return _mapper.Map<UsuarioDTO>(_context.Usuario.Where(x => x.Login == login).FirstOrDefault());
        }
    }
}
