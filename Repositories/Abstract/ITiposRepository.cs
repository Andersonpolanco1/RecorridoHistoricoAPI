using EdecanesV2.Models;

namespace EdecanesV2.Repositories.Abstract
{
    public interface ITiposRepository
    {
        Task<IEnumerable<Tipo>> GetAllAsync();
        Task<Tipo> GetByIdAsync(int id);
        Task<Tipo> CreateAsync(Tipo tipoRecorrido);
        Task DeleteAsync(int id);
        bool TipoRecorridoExists(int id);
    }
}
