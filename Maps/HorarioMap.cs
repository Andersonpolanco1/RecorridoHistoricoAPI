using EdecanesV2.Maps.Base;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdecanesV2.Maps
{
    public class HorarioMap : EntityMapBase<Horario>
    {
        public override void Configure(EntityTypeBuilder<Horario> builder)
        {
            base.Configure(builder);
        }
    }
}
