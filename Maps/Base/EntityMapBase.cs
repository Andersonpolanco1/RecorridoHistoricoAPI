using RecorridoHistoricoApi.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecorridoHistoricoApi.Maps.Base
{

    public class EntityMapBase<TEntity> : IEntityMapBase<TEntity> where TEntity : class, IEntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(t => t.DeletedAt == null);
        }
    }
}
