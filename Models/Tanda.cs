using EdecanesV2.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdecanesV2.Models
{
    [Table("Tandas")]
    public class Tanda : EntityBase
    {
        public string Descripcion { get; set; }
    }
}
