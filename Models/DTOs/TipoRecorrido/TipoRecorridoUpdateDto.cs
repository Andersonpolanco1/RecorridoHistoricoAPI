using System.ComponentModel.DataAnnotations;

namespace EdecanesV2.Models.DTOs.TipoRecorrido
{
    public class TipoRecorridoUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Nombre { get; set; } 

        public bool? EsFlexible { get; set; }

        [MaxLength(10)]
        public string? Color { get; set; }

        public int? CantidadMaxima { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; } 
    }
}
