using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public interface IUsuarioService
    {
        bool Login(UsuarioDTO login);
        UsuarioDTO GetUsuario(string login);
    }
}
