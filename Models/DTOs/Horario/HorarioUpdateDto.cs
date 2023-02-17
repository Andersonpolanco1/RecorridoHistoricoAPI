using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Models.DTOs.Horario
{
    public class HorarioUpdateDto 
    {
        public int Id { get; set; }
        public int? TipoRecorridoId { get; set; }
        public int? TandaId { get; set; }
        public DiaSemana? Dia { get; set; }
        public string? Hora { get; set; } 
    }
}
