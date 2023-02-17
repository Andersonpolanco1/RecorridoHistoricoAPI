using System.ComponentModel.DataAnnotations;

namespace EdecanesV2.Models.DTOs.TipoRecorrido
{
    public class TipoReadDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Nombre { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }
}
