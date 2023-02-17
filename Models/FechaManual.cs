using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using EdecanesV2.Models.Base;

namespace EdecanesV2.Models
{
    [Table("FechasManuales")]
    public class FechaManual : EntityBase
    {

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }

        public bool EsRecurrente { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Comentario { get; set; } = null!;
    }
}
