using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps
{
    public class RecorridoHistoricoMap : EntityMapBase<RecorridoHistorico>
    {
        public override void Configure(EntityTypeBuilder<RecorridoHistorico> builder)
        {
            builder.HasQueryFilter(t => t.DeletedAt == null && t.Estado.DeletedAt == null && t.Horario.DeletedAt == null && t.TipoRecorrido.DeletedAt == null);

        }
    }
}
