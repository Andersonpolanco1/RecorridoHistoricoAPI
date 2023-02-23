using RecorridoHistoricoApi.Data;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.DisponibilidadesDto;
using RecorridoHistoricoApi.Models.DTOs.Horarios;
using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Utils;
using Microsoft.EntityFrameworkCore;
using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Repositories.Impl
{
    public class DisponibilidadesRepository: IDisponibilidadesRepository
    {
        private readonly ApplicationDbContext _context;

        public DisponibilidadesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public FechasNoDisponibles FechasNoDisponibles()
        {
            FechasNoDisponibles fechasNoDisponiblesDto = new();

            var recorridosProgramados = GetRecorridosProgramados();

            var recorridosAgrupadosPorFechaVisita = (from recorrido in recorridosProgramados
                     group recorrido by  recorrido.FechaVisita into recorridos
                     select new
                     {
                         Key = recorridos.Key,
                         horarios = recorridos.Count()
                     }).ToList();
            

            var horariosPorDia = GetCantidadHorariosPorDiaSemana();

            List<DateTime> fechasLlenas = new();
            DiaSemana diaDelRecorrido;
            int cantHorarios;

            foreach (var item in recorridosAgrupadosPorFechaVisita)
            {
                diaDelRecorrido = DateAndTimeUtils.ToEnumDiaSemana(item.Key);
                cantHorarios = horariosPorDia.FirstOrDefault(kvp => kvp.Key == diaDelRecorrido).Value;

                if(item.horarios >= cantHorarios)
                    fechasLlenas.Add(item.Key);
            }

            fechasNoDisponiblesDto.Llenas = fechasLlenas;
            fechasNoDisponiblesDto.ManualesRecurrentes = GetFechasManualesRecurrentes();
            fechasNoDisponiblesDto.ManualesTemporales = GetFechasManualesTemporales();

            return fechasNoDisponiblesDto;
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
                .Where(r => r.FechaVisita >= DateTime.Now).ToList();
        }

        private List<DateTime> GetFechasManualesTemporales()
        {
            return _context.FechasManuales
                .Where(fm => fm.EsRecurrente == false && fm.Fecha >= DateTime.Now)
                .Select(fm => fm.Fecha)
                .ToList();
        }

        private List<DateTime> GetFechasManualesRecurrentes()
        {
            return _context.FechasManuales
                .Where(fm => fm.EsRecurrente == true)
                .Select(fm => fm.Fecha)
                .ToList();
        }

        public IEnumerable<Horario> HorariosDisponibles(DateTime fecha)
        {
            if(fecha < DateTime.Now)
                return new List<Horario>();

            var recorridosProgramados = GetRecorridosProgramados(fecha);
            var horariosDelDia = GetHorariosPorDiaSemana(DateAndTimeUtils.ToEnumDiaSemana(fecha)).ToList();

            List<Horario> horariosDisponibles = new();

            foreach (var horario in horariosDelDia)
            {
                if (!recorridosProgramados.Any(rp => rp.HorarioId == horario.Id))
                    horariosDisponibles.Add(horario);
            }

            return horariosDisponibles;
        }

        private List<RecorridoHistorico> GetRecorridosProgramados(DateTime fecha)
        {
            return _context.RecorridosHistoricos.AsNoTracking()
                .Include(r => r.Horario)
                .Where(r => r.FechaVisita == fecha && r.FechaVisita >= DateTime.Now)
                .ToList();
        }

        private IEnumerable<Horario> GetHorariosPorDiaSemana(DiaSemana dia)
        {
            return _context.Horarios.AsNoTracking().Where(h => h.Dia == dia).ToList();
        }

        public IEnumerable<HorariosTipoRecorridoDto> TiposRecorridoDisponibles(DateTime fecha)
        {

            if (fecha < DateTime.Now)
                return new List<HorariosTipoRecorridoDto>();

            var recorridosProgramados = GetRecorridosProgramados(fecha);
            var tiposRecorridos = TiposRecorridoConHorario(fecha);

            if(!recorridosProgramados.Any())
                return tiposRecorridos;

            List<HorariosTipoRecorridoDto> disponibles = new();

            foreach (var tipo in tiposRecorridos)
            {
                if (recorridosProgramados.Any(rp => rp.TipoRecorridoId == tipo.IdTipoRecorrido && tipo.Horarios.Any(th => th.Id == rp.HorarioId)))
                    continue;
                
                disponibles.Add(tipo);
            }
            return disponibles;
        }

        private IEnumerable<HorariosTipoRecorridoDto> TiposRecorridoConHorario(DateTime fecha)
        {
            return _context.Tipos.AsNoTracking()
                .Include(t => t.Horarios)
                .Where(t => t.Horarios.Any(h => h.Dia == DateAndTimeUtils.ToEnumDiaSemana(fecha)))
                .Select(t => new HorariosTipoRecorridoDto
                {
                    IdTipoRecorrido = t.Id,
                    Nombre = t.Nombre,
                    Horarios = t.Horarios.Where(h => h.Dia == DateAndTimeUtils.ToEnumDiaSemana(fecha))
                        .Select(h => new HorarioDto { Dia = h.Dia, Hora = h.Hora, Id = h.Id, TandaId = h.TandaId} )
                })
                .ToList();
        }
    }
}
