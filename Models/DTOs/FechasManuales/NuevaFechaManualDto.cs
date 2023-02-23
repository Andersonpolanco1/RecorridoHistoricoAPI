using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecorridoHistoricoApi.Models.DTOs.FechasManuales
{
    public class NuevaFechaManualDto
    {
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }

        public bool EsRecurrente { get; set; }
        
        [MaxLength(150)]
        [Required(ErrorMessage = "Escriba un comentario")]
        public string Comentario { get; set; } = null!;
    }
}
