using System.ComponentModel.DataAnnotations;

namespace EdecanesV2.Models.DTOs.FechasManuales
{
    public class ActualizarFechaManualDto
    {
        public int Id { get; set; }

        public bool? EsRecurrente { get; set; } = null;

        [MaxLength(150)]
        public string? Comentario { get; set; } = null;
    }
}
