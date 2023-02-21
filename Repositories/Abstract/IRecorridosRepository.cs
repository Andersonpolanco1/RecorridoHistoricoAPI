using EdecanesV2.Models;

namespace EdecanesV2.Repositories.Abstract
{
    public interface IRecorridosRepository
    {
        Task<IEnumerable<RecorridoHistorico>> GetAllAsync();
        Task<RecorridoHistorico?> GetByIdAsync(int id);
        Task<RecorridoHistorico> CreateAsync(RecorridoHistorico recorridoHistorico);
        Task<RecorridoHistorico> EditAsync(RecorridoHistorico recorridoHistorico);
        bool DeleteAsync(int id);
        bool TipoRecorridoExists(int id);

        void RestoreDeleted(int id);
        IEnumerable<RecorridoHistorico> Deleted();
        RecorridoHistorico? GetDeleted(int id);
    }
}
