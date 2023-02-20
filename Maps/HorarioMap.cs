using EdecanesV2.Maps.Base;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdecanesV2.Maps
{
    public class HorarioMap : EntityMapBase<Horario>
    {
        public override void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasQueryFilter(h =>h.DeletedAt==null && h.Tanda.DeletedAt == null );
        }
    }
}
