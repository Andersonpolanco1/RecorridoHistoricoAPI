using EdecanesV2.Data;
using EdecanesV2.Extensions;
using EdecanesV2.Models;
using EdecanesV2.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EdecanesV2.Repositories.Impl
{
    public class FechasManualesRepository : IFechasManualesRepository
    {
        private readonly ApplicationDbContext _context;

        public FechasManualesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FechaManual>> GetAllAsync()
        {
            return await _context.FechasManuales.AsNoTracking().ToListAsync();
        }


        public async Task<FechaManual> CreateAsync(FechaManual fechaManual)
        {
            if (fechaManual.Fecha < DateTime.Now)
            {
                throw new ArgumentException($"No se pueden registrar fechas pasadas. Fecha: ({fechaManual.Fecha.ToShortDateString()})");
            }
            else if (FechaManualExiste(fechaManual))
            {
                throw new ArgumentException($"La fecha {fechaManual.Fecha.ToShortDateString()} no esta disponible.");
            }

            fechaManual.FechaRegistro = DateTime.Now;
            _context.Add(fechaManual);
            await _context.SaveChangesAsync();
            return fechaManual;
        }

        public async Task<FechaManual?> GetAsync(int id)
        {
            var fecha = await _context.FechasManuales.FirstOrDefaultAsync(x => x.Id == id);

            if (fecha == null)
                throw new NullReferenceException("No se encontro el registro");

            return fecha;
        }

        public async Task<FechaManual> EditAsync(FechaManual fechaManual)
        {
            if (fechaManual is null)
                throw new ArgumentException(nameof(fechaManual));

            if(FechaManualExiste(fechaManual))
                throw new ArgumentException($"La fecha {fechaManual.Fecha.ToShortDateString()} no esta disponible.");

            _context.Update(fechaManual);
            await _context.SaveChangesAsync();

            return fechaManual;
        }

        public async Task DeleteAsync(int id)
        {
            var fechaManual = await _context.FechasManuales.FirstOrDefaultAsync(x => x.Id == id);

            if (fechaManual == null)
                throw new NullReferenceException("No se pudo eliminar.");

            _context.FechasManuales.Remove(fechaManual);
            await _context.SaveChangesAsync();
        }

        public bool FechaManualExists(int id)
        {
            return _context.FechasManuales.Any(x => x.Id == id);
        }

        public bool FechaManualExiste(FechaManual fecha)
        {
            return _context.FechasManuales.Any(f => f.Fecha == fecha.Fecha && f.EsRecurrente == fecha.EsRecurrente);
        }


        public void RestoreDeleted(int id)
        {
            try
            {
                _context.FechasManuales.RestoreDeleted(id);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<FechaManual> Deleted()
        {
            return _context.FechasManuales.IgnoreQueryFilters().Where(t => t.DeletedAt.HasValue).ToList();
        }

        public FechaManual? GetDeleted(int id)
        {
            return _context.FechasManuales.IgnoreQueryFilters().FirstOrDefault(t => t.Id == id && t.DeletedAt.HasValue);
        }
    }
}
