using System.ComponentModel.DataAnnotations;

namespace RecorridoHistoricoApi.Models.DTOs.TipoRecorrido
{
    public class TipoUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Nombre { get; set; }

        public bool? EsFlexible { get; set; } = null!;

        [MaxLength(10)]
        public string? Color { get; set; }

        public int? CantidadMaxima { get; set; } = null;

        [MaxLength(255)]
        public string? Descripcion { get; set; } 
    }
}
