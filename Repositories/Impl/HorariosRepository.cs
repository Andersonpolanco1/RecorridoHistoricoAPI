using EdecanesV2.Data;
using EdecanesV2.Extensions;
using EdecanesV2.Models;
using EdecanesV2.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Repositories.Impl
{
    public class HorariosRepository : IHorariosRepository
    {
        private readonly ApplicationDbContext _context;

        public HorariosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Horario>> GetAllAsync()
        {
            return await _context.Horarios.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Horario> GetByIdAsync(int HorarioId)
        {
            var horarioRecorrido = await _context.Horarios.FirstOrDefaultAsync(m => m.Id == HorarioId);

            if (horarioRecorrido == null)
                throw new NullReferenceException("No se encontro el registro");

            return horarioRecorrido;
        }

        public async Task<Horario> CreateAsync(Horario horarioRecorrido)
        {
            ValidarHorario(horarioRecorrido);
            _context.Add(horarioRecorrido);
            await _context.SaveChangesAsync();
            return horarioRecorrido;
        }

        public async Task<Horario> EditAsync(Horario horarioRecorrido)
        {
            ValidarHorario(horarioRecorrido);
            _context.Update(horarioRecorrido);
            await _context.SaveChangesAsync();
            return horarioRecorrido;
        }

        public async Task DeleteAsync(int id)
        {
            var horarioRecorrido = _context.Horarios.FirstOrDefault(x => x.Id == id);

            if (horarioRecorrido == null)
                throw new NullReferenceException("No se pudo eliminar.");

            _context.Horarios.Remove(horarioRecorrido);

            await _context.SaveChangesAsync();
        }

        public void ValidarHorario(Horario horarioRecorrido)
        {
            if (!_context.Tandas.Any(t => t.Id == horarioRecorrido.TandaId))
                throw new ArgumentException("Tanda no valida.");

            //if (!_context.Tipos.Any(t => t.Id == horarioRecorrido.TipoRecorridoId))
            //    throw new ArgumentException("Tipo de recorrido no válido.");

            if (!Enum.IsDefined(typeof(DiaSemana), horarioRecorrido.Dia))
                throw new ArgumentException("Día de semana no válido.");

            if(_context.Horarios.Any(h =>
              //  h.TipoRecorridoId == horarioRecorrido.TipoRecorridoId &&
                h.Dia == horarioRecorrido.Dia &&
                h.TandaId == horarioRecorrido.TandaId &&
                h.Hora == horarioRecorrido.Hora))
            {
                string msgError = new StringBuilder(150)
                   .Append("Este tipo de recorrido ya tiene registrado el día ")
                   .Append(horarioRecorrido.Dia.ToString().ToLower())
                   .Append(" en tanda ")
                   .Append(horarioRecorrido.TandaId)
                   .Append($" hora {horarioRecorrido.Hora}")
                   .ToString();

                throw new ArgumentException(msgError);
            }
        }

        public bool EsTandaValida(int tandaId)
        {
            return _context.Tandas.Any(t => t.Id == tandaId);
        }

        public async Task<IEnumerable<Horario>> GetHorariosByTipoRecorrido(int tandaId, int tipoRecorridoId)
        {
            var query = from horario in _context.Horarios
                        where horario.TiposRecorrido.Any(h => h.Id == tipoRecorridoId)
                        select horario;

            return query.Where(h => h.TandaId == tandaId);
                        
        }

        public void RestoreDeleted(int id)
        {
            try
            {
                _context.Horarios.RestoreDeleted(id);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Horario> Deleted()
        {
            return _context.Horarios.IgnoreQueryFilters().Where(t => t.DeletedAt.HasValue).ToList();
        }

        public Horario? GetDeleted(int id)
        {
            return _context.Horarios.IgnoreQueryFilters().FirstOrDefault(t => t.Id == id && t.DeletedAt.HasValue);
        }
    }
}
