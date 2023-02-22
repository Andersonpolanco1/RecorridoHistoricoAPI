using EdecanesV2.Data;
using EdecanesV2.Models;
using EdecanesV2.Utils;
using Microsoft.EntityFrameworkCore;
using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Repositories.Impl
{
    public class DisponibilidadesRepository
    {
        private readonly ApplicationDbContext _context;

        public DisponibilidadesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> FechasNoDisponibles()
        {
            var recorridosProgramados = GetRecorridosProgramados();


            var recorridosAgrup = (from recorrido in recorridosProgramados
                     group recorrido by  recorrido.FechaVisita into recorridos
                     select new
                     {
                         Key = recorridos.Key,
                         horarios = recorridos.Count()
                     }).ToList();


            var horariosPorDia = GetCantidadHorariosPorDiaSemana();

            List<string> fechasNoDisponibles = new();

            foreach (var item in recorridosAgrup)
            {
                var dia = DateAndTimeUtils.ToEnumDiaSemana(item.Key);
                var cantMax = horariosPorDia.FirstOrDefault(kvp => kvp.Key == dia).Value;

                if(item.horarios >= cantMax)
                    fechasNoDisponibles.Add(item.Key.ToShortDateString());
            }

            return fechasNoDisponibles;
        }

        private List<KeyValuePair<DiaSemana, int>> GetCantidadHorariosPorDiaSemana()
        {
            List<KeyValuePair<DiaSemana, int>> horariosPorDia = new();

            var h = (from horario in _context.Horarios
                    group horario by horario.Dia into dias
                    select new
                    {
                        Key = dias.Key,
                        horarios = dias.Count()
                    }).ToList();

            foreach (var item in h)
            {
                horariosPorDia.Add(new KeyValuePair<DiaSemana, int>(item.Key, item.horarios));
            }

            return horariosPorDia;

        }

        private List<RecorridoHistorico> GetRecorridosProgramados()
        {
            return _context.RecorridosHistoricos.AsNoTracking()
                .Include(r => r.Horario)
                .Include(r => r.TipoRecorrido)
                .Where(r => r.FechaVisita >= DateTime.Now).ToList();
        }

    }
}
