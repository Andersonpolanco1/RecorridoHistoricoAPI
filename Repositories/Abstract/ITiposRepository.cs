using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace RecorridoHistoricoApi.Repositories.Abstract
{
    public interface ITiposRepository
    {
        Task<IEnumerable<Tipo>> GetAllAsync();
        Task<Tipo> GetByIdAsync(int id);
        Task<Tipo> CreateAsync(Tipo tipoRecorrido);
        Task<Tipo> EditAsync(Tipo tipoRecorrido);
        Task DeleteAsync(int id);
        bool TipoRecorridoExists(int id);
        void AddHorario(int tipoId, int horarioId);
        void RemoveHorario(int tipoId, int horarioId);
        void RestoreDeleted(int id);
        IEnumerable<Tipo> Deleted();
        Tipo? GetDeleted(int id);
        IEnumerable<Horario> GetHorarios(int tipoId);

    }
}
