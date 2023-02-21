using EdecanesV2.Data;
using EdecanesV2.Models;
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


            //TODO

            return null;
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
