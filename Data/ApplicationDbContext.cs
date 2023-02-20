using EdecanesV2.Extensions;
using EdecanesV2.Maps;
using EdecanesV2.Models;
using EdecanesV2.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EdecanesV2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<FechaManual> FechasManuales { get; set; }
        public virtual DbSet<RecorridoHistorico> RecorridosHistoricos { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }
        public virtual DbSet<Horario> Horarios { get; set; }
        public virtual DbSet<Tanda> Tandas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new FechaManualMap());
            modelBuilder.ApplyConfiguration(new HorarioMap());
            modelBuilder.ApplyConfiguration(new RecorridoHistoricoMap());
            modelBuilder.ApplyConfiguration(new TandaMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
