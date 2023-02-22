using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.DisponibilidadesDto;

namespace EdecanesV2.Repositories.Abstract
{
    public interface IDisponibilidadesRepository
    {
        IEnumerable<string> FechasNoDisponibles();
        IEnumerable<Horario> HorariosDisponibles(DateTime fecha);
        IEnumerable<HorariosTipoRecorridoDto> TiposRecorridoDisponibles(DateTime fecha);
    }
}
