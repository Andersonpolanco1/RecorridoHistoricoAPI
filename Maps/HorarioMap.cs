using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps
{
    public class HorarioMap : EntityMapBase<Horario>
    {
        public override void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasQueryFilter(h =>h.DeletedAt==null && h.Tanda.DeletedAt == null );
        }
    }
}
