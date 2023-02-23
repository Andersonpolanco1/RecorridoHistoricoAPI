using RecorridoHistoricoApi.Models.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using RecorridoHistoricoApi.Models;

namespace RecorridoHistoricoApi.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            SetAuditValuesWhenDeleted(changeTracker);
        }

        private static void SetAuditValuesWhenDeleted(ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            IEnumerable<EntityEntry> entities =
                changeTracker
                    .Entries()
                    .Where(t => t.Entity is IEntityBase && t.State == EntityState.Deleted);

            if (entities.Any())
            {
                foreach (EntityEntry entry in entities)
                {
                    IEntityBase entity = (IEntityBase)entry.Entity;
                    entity.DeletedAt = DateTime.Now;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}

