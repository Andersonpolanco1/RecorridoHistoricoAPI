using RecorridoHistoricoApi.Data;
using RecorridoHistoricoApi.Extensions;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RecorridoHistoricoApi.Repositories.Impl
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

        public void AddHorario(int tipoId, int horarioId)
        {
            var tipo = _context.Tipos.Include(t => t.Horarios).FirstOrDefault(t => t.Id == tipoId);

            if (tipo == null)
                throw new Exception("Tipo de recorrido no valido");

            var horario = _context.Horarios.FirstOrDefault(h => h.Id == horarioId);

            if (horario == null)
                throw new Exception("Horario no valido");


            if (tipo.Horarios.Any(h => h.Id == horario.Id))
                throw new Exception($"Tipo de recorrido ya tiene el horario {horario.Descripcion()}");

            tipo.Horarios.Add(horario);
            _context.SaveChanges();
        }

        public void RemoveHorario(int tipoId, int horarioId)
        {
            var tipo = _context.Tipos.Include(t => t.Horarios).FirstOrDefault(t => t.Id == tipoId);

            if (tipo == null)
                throw new Exception("Tipo de recorrido no valido");

            var horario = _context.Horarios.FirstOrDefault(h => h.Id == horarioId);

            if (horario == null)
                throw new Exception("Horario no valido");


            if (!tipo.Horarios.Any(h => h.Id == horario.Id))
                throw new Exception($"No se pudo remover horario");

            tipo.Horarios.Remove(horario);
            _context.SaveChanges();
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

        public IEnumerable<Horario> GetHorarios(int tipoId)
        {
            var query = from horario in _context.Horarios
                        where horario.TiposRecorrido.Any(h => h.Id == tipoId)
                        select horario;

            return query.ToList();
        }
    }
}

