using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico;
using RecorridoHistoricoApi.Utils.DataTable;

namespace RecorridoHistoricoApi.Repositories.Abstract
{
    public interface IDashboardEdecanesRepository
    {
        DtResult<RecorridoDataTableDashboard> GetSolicitudesDtAsync(DtParameters dtParameters);
        Task<IEnumerable<EstadisticaDto>> GetEstadisticaEstadosAsync();
        Task<IEnumerable<EstadisticaDto>> GetEstadisticaTiposAsync();
        Task<IEnumerable<RecorridoCalendarioDashboardDto>> GetRecorridosCalendarioDashboard();
    }
}
