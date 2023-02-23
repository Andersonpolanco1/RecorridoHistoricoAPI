using RecorridoHistoricoApi.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace RecorridoHistoricoApi.Extensions
{
    public static class DbContextExtensions
    {
        public static void RestoreDeleted<TModel>(this DbSet<TModel> model, int id) where TModel : EntityBase
        {
            var entityToRecover = model.IgnoreQueryFilters().FirstOrDefault(e => e.Id == id && e.DeletedAt.HasValue);

            if (entityToRecover == null)
                throw new InvalidOperationException("Entity can not recover");

            entityToRecover.DeletedAt = null;
        }
    }
}
