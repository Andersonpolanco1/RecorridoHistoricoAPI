using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps
{
    public class EstadoMap : EntityMapBase<Estado>
    {
        public override void Configure(EntityTypeBuilder<Estado> builder)
        {
            base.Configure(builder);
        }
    }
}
