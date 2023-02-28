using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecorridoHistoricoApi.Maps.Base;
using RecorridoHistoricoApi.Models;

namespace RecorridoHistoricoApi.Maps
{
    public class EmpleadoMap : EntityMapBase<Empleado>
    {
        public override void Configure(EntityTypeBuilder<Empleado> builder)
        {
            base.Configure(builder);
        }
    }
}