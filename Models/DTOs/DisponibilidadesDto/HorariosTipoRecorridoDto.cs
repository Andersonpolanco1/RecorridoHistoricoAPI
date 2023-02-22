using EdecanesV2.Models.DTOs.Horarios;

namespace EdecanesV2.Models.DTOs.DisponibilidadesDto
{
    public class HorariosTipoRecorridoDto
    {
        public int IdTipoRecorrido { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public IEnumerable<HorarioDto> Horarios { get; set; } = new List<HorarioDto>();

    }
}
