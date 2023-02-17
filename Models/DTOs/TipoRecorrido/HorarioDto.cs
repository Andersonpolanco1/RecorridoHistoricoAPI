using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Models.DTOs.TipoRecorrido
{
    public class HorarioDto
    {
        public int Id { get; set; }

        public DiaSemana Dia { get; set; }

        public string Hora { get; set; } = string.Empty;
    }
}
