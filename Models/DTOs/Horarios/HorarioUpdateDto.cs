using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Models.DTOs.Horarios
{
    public class HorarioUpdateDto 
    {
        public int Id { get; set; }
        public int? TandaId { get; set; }
        public DiaSemana? Dia { get; set; }
        public string? Hora { get; set; } 
    }
}
