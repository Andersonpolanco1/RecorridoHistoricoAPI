using EdecanesV2.Maps.Base;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdecanesV2.Maps
{
    public class RecorridoHistoricoMap : EntityMapBase<RecorridoHistorico>
    {
        public override void Configure(EntityTypeBuilder<RecorridoHistorico> builder)
        {
            base.Configure(builder);
        }
    }
}
