using EdecanesV2.Maps.Base;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdecanesV2.Maps
{
    public class TandaMap : EntityMapBase<Tanda>
    {
        public override void Configure(EntityTypeBuilder<Tanda> builder)
        {
            base.Configure(builder);
        }
    }
}
