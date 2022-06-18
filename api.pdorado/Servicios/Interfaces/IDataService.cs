using api.pdorado.Data.Models;
using pdorado.data.Models;

namespace api.pdorado.Servicios.Interfaces
{
    public interface IDataService<DTO, DB> where DTO : BaseDTO
        where DB : BaseDB
    {
        Task<List<DTO>> GetAll(int idLenguaje);
        Task<DTO> Get(int id, int idLenguaje);
        Task<DTO> Update(int id, int idLenguaje, DTO dto);
        Task<DTO> Create(int idLenguaje, DTO dto);
        Task<bool> Delete(int id);
    }
}
