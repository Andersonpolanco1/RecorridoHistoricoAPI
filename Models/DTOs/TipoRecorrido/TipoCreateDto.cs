using System.ComponentModel.DataAnnotations;

namespace EdecanesV2.Models.DTOs.TipoRecorrido
{
    public class TipoCreateDto
    {
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool EsFlexible { get; set; }

        [MaxLength(10)]
        public string? Color { get; set; }

        public int CantidadMaxima { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; } 

    }
}
