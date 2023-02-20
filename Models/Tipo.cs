using EdecanesV2.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdecanesV2.Models
{
    [Table("Tipos")]
    public class Tipo : EntityBase
    {
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool EsFlexible { get; set; }

        [MaxLength(10)]
        public string? Color { get; set; }

        public int CantidadMaxima { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public List<Horario> Horarios { get; set; } = new List<Horario>();

    }
}
