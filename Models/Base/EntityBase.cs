using System.ComponentModel.DataAnnotations.Schema;

namespace EdecanesV2.Models.Base
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
