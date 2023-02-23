using RecorridoHistoricoApi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecorridoHistoricoApi.Models
{
    [Table("Estados")]
    public class Estado : EntityBase
    {
        public string Nombre { get; set; }
    }
}

