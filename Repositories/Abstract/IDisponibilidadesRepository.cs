using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.DisponibilidadesDto;

namespace RecorridoHistoricoApi.Repositories.Abstract
{
    public interface IDisponibilidadesRepository
    {
        IEnumerable<string> FechasNoDisponibles();
        IEnumerable<Horario> HorariosDisponibles(DateTime fecha);
        IEnumerable<HorariosTipoRecorridoDto> TiposRecorridoDisponibles(DateTime fecha);
    }
}
