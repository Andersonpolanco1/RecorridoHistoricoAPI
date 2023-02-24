using RecorridoHistoricoApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecorridoHistoricoApi.Models
{
    [Table("Tipos")]
    public class Tipo : EntityBase
    {
        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Esta propiedad en true significa que este tipo de recorrido es flexible con la disponibilidad, permite varias solicitudes el mismo día y hora.
        /// </summary>
        public bool EsFlexible { get; set; }

        [StringLength(50, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Color { get; set; }

        [Range(10, 300, ErrorMessage = "Valor de {0} debe estar entre {1} y {2}.")]
        public int CantidadMaxima { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public List<Horario> Horarios { get; set; } = new List<Horario>();

    }
}
