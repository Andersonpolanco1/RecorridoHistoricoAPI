using Microsoft.EntityFrameworkCore;

namespace RecorridoHistoricoApi.Maps.Base
{
    public interface IEntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
    }
}
