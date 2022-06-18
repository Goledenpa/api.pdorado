using pdorado.data.Models;

namespace api.pdorado.Servicios.Interfaces
{
    public interface IUsuarioService
    {
        bool Login(UsuarioDTO login);
        Task<UsuarioDTO> GetUsuario(string login);
        Task<List<UsuarioDTO>> GetAll();
        Task<UsuarioDTO> CreateUsuario(UsuarioDTO dto);
        Task<UsuarioDTO> UpdateUsuario(string login, UsuarioDTO dto);
        Task<bool> DeleteUsuario(string login);
    }
}
