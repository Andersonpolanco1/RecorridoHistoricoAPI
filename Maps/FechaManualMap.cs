using EdecanesV2.Maps.Base;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdecanesV2.Maps
{
    public class FechaManualMap : EntityMapBase<FechaManual>
    {
        public override void Configure(EntityTypeBuilder<FechaManual> builder)
        {
            base.Configure(builder);
        }
    }
}
