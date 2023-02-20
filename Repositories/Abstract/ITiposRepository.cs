using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore;

namespace EdecanesV2.Repositories.Abstract
{
    public interface ITiposRepository
    {
        Task<IEnumerable<Tipo>> GetAllAsync();
        Task<Tipo> GetByIdAsync(int id);
        Task<Tipo> CreateAsync(Tipo tipoRecorrido);
        Task<Tipo> EditAsync(Tipo tipoRecorrido);
        Task DeleteAsync(int id);
        bool TipoRecorridoExists(int id);

        void RestoreDeleted(int id);
        IEnumerable<Tipo> Deleted();
        Tipo? GetDeleted(int id);

    }
}
