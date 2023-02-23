using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using RecorridoHistoricoApi.Models.Base;

namespace RecorridoHistoricoApi.Models
{
    [Table("FechasManuales")]
    public class FechaManual : EntityBase
    {
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }

        public bool EsRecurrente { get; set; }

        public DateTime FechaRegistro { get; set; }

        [StringLength(150, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Comentario { get; set; } = null!;
    }
}
