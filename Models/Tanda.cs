using RecorridoHistoricoApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecorridoHistoricoApi.Models
{
    [Table("Tandas")]
    public class Tanda : EntityBase
    {
        [StringLength(150, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Descripcion { get; set; }
    }
}
