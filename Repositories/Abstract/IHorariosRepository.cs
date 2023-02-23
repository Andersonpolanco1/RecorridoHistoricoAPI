using RecorridoHistoricoApi.Models;

namespace RecorridoHistoricoApi.Repositories.Abstract
{
    public interface IHorariosRepository
    {
        Task<IEnumerable<Horario>> GetAllAsync();
        Task<Horario> GetByIdAsync(int horarioId);
        Task<Horario> CreateAsync(Horario horarioRecorrido);
        Task<Horario> EditAsync(Horario horarioRecorrido);
        Task DeleteAsync(int horarioId);
        bool EsTandaValida(int tandaId);
        IEnumerable<Horario> GetHorariosByTipoRecorrido(int tandaId, int tipoRecorridoId);
        void RestoreDeleted(int id);
        IEnumerable<Horario> Deleted();
        Horario? GetDeleted(int id);
    }
}
