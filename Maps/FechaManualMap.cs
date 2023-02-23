using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps
{
    public class FechaManualMap : EntityMapBase<FechaManual>
    {
        public override void Configure(EntityTypeBuilder<FechaManual> builder)
        {
            base.Configure(builder);
        }
    }
}
