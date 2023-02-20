using EdecanesV2.Data;
using EdecanesV2.Extensions;
using EdecanesV2.Models;
using EdecanesV2.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdecanesV2.Repositories.Impl
{
    public class TipoRepository : ITiposRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Tipo>> GetAllAsync()
        {
            return await _context.Tipos.ToListAsync();
        }


        public async Task<Tipo> GetByIdAsync(int id)
        {
            var tipoRecorrido = await _context.Tipos.FirstOrDefaultAsync(m => m.Id == id);

            if (tipoRecorrido == null)
                throw new NullReferenceException("No se pudo obtener el registro.");

            return tipoRecorrido;
        }


        public async Task<Tipo> CreateAsync(Tipo tipoRecorrido)
        {
            if (_context.Tipos.Any(tipo => tipo.Nombre.Equals(tipoRecorrido.Nombre)))
                throw new ArgumentException("Tipo de recorrido ya existe");

            _context.Add(tipoRecorrido);
            await _context.SaveChangesAsync();
            return tipoRecorrido;
        }

        public async Task<Tipo> EditAsync(Tipo tipoRecorrido)
        {
            if (_context.Tipos.Any(tipo => tipo.Nombre.Equals(tipoRecorrido.Nombre) && tipo.Id != tipoRecorrido.Id))
                throw new ArgumentException("Existe un tipo de recorrido con el mismo nombre.");

            _context.Update(tipoRecorrido);
            await _context.SaveChangesAsync();

            return tipoRecorrido;
        }

        public async Task DeleteAsync(int id)
        {
            var tipoRecorrido = _context.Tipos.FirstOrDefault(tipo => tipo.Id == id);

            if (tipoRecorrido == null)
                throw new NullReferenceException("No se pudo eliminar.");

            _context.Tipos.Remove(tipoRecorrido);
            await _context.SaveChangesAsync();
        }

        public bool TipoRecorridoExists(int id)
        {
            return _context.Tipos.Any(e => e.Id == id);
        }


        public void RestoreDeleted(int id)
        {
            try
            {
                _context.Tipos.RestoreDeleted(id);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Tipo> Deleted()
        {
            return _context.Tipos.IgnoreQueryFilters().Where(t => t.DeletedAt.HasValue).ToList();
        }

        public Tipo? GetDeleted(int id)
        {
            return _context.Tipos.IgnoreQueryFilters().FirstOrDefault(t => t.Id == id && t.DeletedAt.HasValue);
        }
    }
}

