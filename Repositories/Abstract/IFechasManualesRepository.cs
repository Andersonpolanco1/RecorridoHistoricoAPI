using EdecanesV2.Models;

namespace EdecanesV2.Repositories.Abstract
{
    public interface IFechasManualesRepository
    {
        Task<IEnumerable<FechaManual>> GetAllAsync();
        Task<FechaManual> CreateAsync(FechaManual fechaManual);
        Task<FechaManual?> GetAsync(int id);
        Task<FechaManual> EditAsync(FechaManual fechaManual);
        Task DeleteAsync(int id);

        void RestoreDeleted(int id);
        IEnumerable<FechaManual> Deleted();
        FechaManual? GetDeleted(int id);
    }
}
