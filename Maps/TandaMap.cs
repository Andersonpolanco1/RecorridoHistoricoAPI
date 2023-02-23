using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps
{
    public class TandaMap : EntityMapBase<Tanda>
    {
        public override void Configure(EntityTypeBuilder<Tanda> builder)
        {
            base.Configure(builder);
        }
    }
}
