using System.ComponentModel.DataAnnotations;

namespace RecorridoHistoricoApi.Models.DTOs.TipoRecorrido
{
    public class TipoCreateDto
    {
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool EsFlexible { get; set; }

        [MaxLength(10)]
        public string? Color { get; set; }

        [Range(10, 300, ErrorMessage = "Valor de {0} debe estar entre {1} y {2}.")]
        public int CantidadMaxima { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; } 

    }
}
