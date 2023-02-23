using System.ComponentModel.DataAnnotations;

namespace RecorridoHistoricoApi.Models.DTOs.FechasManuales
{
    public class ActualizarFechaManualDto
    {
        public int Id { get; set; }

        public bool? EsRecurrente { get; set; } = null;

        [MaxLength(150)]
        public string? Comentario { get; set; } = null;
    }
}
