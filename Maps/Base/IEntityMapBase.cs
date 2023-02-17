using Microsoft.EntityFrameworkCore;

namespace EdecanesV2.Maps.Base
{
    public interface IEntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
    }
}
