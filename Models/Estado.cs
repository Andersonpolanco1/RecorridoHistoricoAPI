using EdecanesV2.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdecanesV2.Models
{
    [Table("Estados")]
    public class Estado : EntityBase
    {
        public string Nombre { get; set; }
    }
}

