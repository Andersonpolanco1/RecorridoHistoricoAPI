using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.RecorridoHistorico;
using EdecanesV2.Utils.DataTable;

namespace EdecanesV2.Repositories.Abstract
{
    public interface IDashboardEdecanesRepository
    {
        DtResult<RecorridoHistorico> GetSolicitudesDtAsync(DtParameters dtParameters);
        Task<IEnumerable<EstadisticaDto>> GetEstadisticaEstadosAsync();
        Task<IEnumerable<EstadisticaDto>> GetEstadisticaTiposAsync();
        Task<IEnumerable<RecorridoCalendarioDashboardDto>> GetRecorridosCalendarioDashboard();
    }
}
