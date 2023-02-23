using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps
{
    public class TipoMap : EntityMapBase<Tipo>
    {
        public override void Configure(EntityTypeBuilder<Tipo> builder)
        {
            base.Configure(builder);
        }
    }
}
