using EdecanesV2.Maps.Base;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdecanesV2.Maps
{
    public class TipoMap : EntityMapBase<Tipo>
    {
        public override void Configure(EntityTypeBuilder<Tipo> builder)
        {
            base.Configure(builder);
        }
    }
}
