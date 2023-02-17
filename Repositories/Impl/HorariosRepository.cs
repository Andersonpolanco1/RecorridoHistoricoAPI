using EdecanesV2.Data;
using EdecanesV2.Models;
using EdecanesV2.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                .Include(t => t.TipoRecorrido)
                .OrderBy(x => x.TipoRecorridoId)
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
            if (!EsHorarioValido(horarioRecorrido))
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
            else
            {
                _context.Add(horarioRecorrido);
                await _context.SaveChangesAsync();
                return horarioRecorrido;
            }
        }

        public async Task<Horario> EditAsync(Horario horarioRecorrido)
        {
            if (!_context.Horarios.Any(h => h.Id == horarioRecorrido.Id))
                throw new NullReferenceException("No se pudo actualizar.");


            if (!EsHorarioValido(horarioRecorrido))
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
            else
            {
                _context.Update(horarioRecorrido);
                await _context.SaveChangesAsync();
                return horarioRecorrido;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var horarioRecorrido = _context.Horarios.FirstOrDefault(x => x.Id == id);

            if (horarioRecorrido == null)
                throw new NullReferenceException("No se pudo eliminar.");

            _context.Horarios.Remove(horarioRecorrido);

            await _context.SaveChangesAsync();
        }

        public bool EsHorarioValido(Horario horarioRecorrido)
        {
            var horarios = _context.Horarios
                .AsNoTracking()
                .Where(h => h.TipoRecorridoId == horarioRecorrido.TipoRecorridoId)
                .ToList();

            if (!horarios.Any(h => h.Id == horarioRecorrido.Id))
                return false;

            var horarioBD = horarios.First(h => h.Id == horarioRecorrido.Id);

            if (horarioBD.Id == horarioRecorrido.Id && horarioBD.TandaId == horarioRecorrido.TandaId) //si está actualizando en la misma tanda
            {
                if (horarioBD.Dia == horarioRecorrido.Dia) //si está actualizando en la misma tanda y mismo dia semana
                    return !(horarioBD.Hora == horarioRecorrido.Hora); // si está actualizando en la misma tanda, mismo dia semana y hora
                else
                    return !horarios.Any(h => h.Dia == horarioRecorrido.Dia && h.Hora == horarioRecorrido.Hora);
            }
            else
                return !horarios.Any(h => h.TandaId == horarioRecorrido.TandaId && h.Dia == horarioRecorrido.Dia && h.Hora == horarioRecorrido.Hora);

        }

        public async Task<IEnumerable<Horario>> GetHorariosByTipoRecorridoIdAsync(int tipoRecorridoId)
        {
            return await _context.Horarios
                .Where(h => h.TipoRecorridoId == tipoRecorridoId)
                .ToListAsync();
        }

        public bool EsTandaValida(int tandaId)
        {
            return _context.Tandas.Any(t => t.Id == tandaId);
        }

        public async Task<Tipo?> GetTipoRecorridoDeHorario(int horarioId)
        {
            var horario = await _context.Horarios
                .Include(h => h.TipoRecorrido)
                .FirstOrDefaultAsync(h => h.Id == horarioId);

            if (horario == null)
                throw new NullReferenceException("No se encontro el registro");

            return horario.TipoRecorrido;
        }

        public async Task<IEnumerable<Horario>> GetHorariosByTipoRecorrido(int tandaId, int tipoRecorridoId)
        {
            return await _context.Horarios
                 .Include(h => h.TipoRecorrido)
                 .Where(h => h.TandaId == tandaId && h.TipoRecorridoId == tipoRecorridoId)
                 .ToListAsync();
        }
    }
}
