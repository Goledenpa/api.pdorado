using pdorado.data.Models;

namespace api.pdorado.Servicios.Interfaces
{
    public interface IUsuarioService
    {
        bool Login(UsuarioDTO login);
        UsuarioDTO GetUsuario(string login);
    }
}
