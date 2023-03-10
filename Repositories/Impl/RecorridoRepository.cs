using AutoMapper.Internal;
using AutoMapper;
using RecorridoHistoricoApi.Data;
using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Services.Abstract;
using RecorridoHistoricoApi.Extensions;
using RecorridoHistoricoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualBasic;
using RecorridoHistoricoApi.Utils;
using static RecorridoHistoricoApi.Models.Horario;
using RecorridoHistoricoApi.Services;
using System.Reflection.Metadata;

namespace RecorridoHistoricoApi.Repositories.Impl
{
    public class RecorridoRepository : IRecorridosRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IDisponibilidadesRepository _disponibilidades;

        public RecorridoRepository(ApplicationDbContext context, IEmailService emailService, IDisponibilidadesRepository disponibilidades)
        {
            _context = context;
            _emailService = emailService;
            _disponibilidades = disponibilidades;
        }

        public async Task<IEnumerable<RecorridoHistorico>> GetAllAsync()
        {
            return await _context.RecorridosHistoricos.ToListAsync();
        }

        public async Task<RecorridoHistorico?> GetByIdAsync(int id)
        {
            return await _context.RecorridosHistoricos.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RecorridoHistorico> CreateAsync(RecorridoHistorico recorridoHistorico)
        {
            if (recorridoHistorico == null)
                throw new ArgumentNullException(nameof(recorridoHistorico));

            var tipoRecorridoDb = _context.Tipos
                .AsNoTracking()
                .Include(t => t.Horarios)
                .FirstOrDefault(t => t.Id == recorridoHistorico.TipoRecorridoId);

            ValidarHorario(tipoRecorridoDb, recorridoHistorico);
              
            if ((!tipoRecorridoDb.EsFlexible) && !FechaVisitaEstaDisponible(recorridoHistorico))
                throw new Exception("Fecha no disponible.");

            const int creado = 1;
            recorridoHistorico.EstadoId = creado;
            recorridoHistorico.FechaCreacion = DateTime.Now;
            _context.RecorridosHistoricos.Add(recorridoHistorico);

            const int affectedRows = 0;
            bool success = _context.SaveChanges() > affectedRows;

            if (!success)
                throw new Exception("Ocurrió un error al momento de guardar el recorrido histórico");

            await _emailService.SendEmailRecorridoCreadoAsync(recorridoHistorico);
            return recorridoHistorico;
        }

        private void ValidarHorario(Tipo? tipoRecorridoDb, RecorridoHistorico newRecorrido)
        {
            if (tipoRecorridoDb == null)
                throw new Exception("Tipo de recorrido no valido");

            if (!tipoRecorridoDb.Horarios.Any(h => h.Id == newRecorrido.HorarioId))
                throw new Exception($"Tipo de recorrido {tipoRecorridoDb.Descripcion} no tiene asignado el horario seleccionado.");


            var dia = Util.ToEnumDiaSemana(newRecorrido.FechaVisita);

            if (!tipoRecorridoDb.Horarios.Any(h => h.Dia == dia))
                throw new Exception("Tipo de recorrido u horario no valido");

            if (!tipoRecorridoDb.Horarios.Any(h => h.Id == newRecorrido.HorarioId))
                throw new Exception($"Tipo de recorrido {tipoRecorridoDb.Descripcion} no disponible el dia {dia}.");

        }

        private bool FechaVisitaEstaDisponible(RecorridoHistorico recorrido)
        {
            var fechasNoDisponibles = _disponibilidades.FechasNoDisponibles();

            if (fechasNoDisponibles.Llenas.Any(f => f == recorrido.FechaVisita))
                return false;

            if (fechasNoDisponibles.ManualesTemporales.Any(f => f == recorrido.FechaVisita))
                return false;

            if (fechasNoDisponibles.ManualesRecurrentes.Any(f => f.Month == recorrido.FechaVisita.Month && f.Day == recorrido.FechaVisita.Day))
                return false;
            
            return true;
        }

        public async Task<RecorridoHistorico> EditAsync(RecorridoHistorico recorridoHistorico)
        {
            if (recorridoHistorico == null)
                throw new ArgumentNullException(nameof(recorridoHistorico));

            var tipoRecorridoDb = _context.Tipos
                .AsNoTracking()
                .Include(t => t.Horarios)
                .FirstOrDefault(t => t.Id == recorridoHistorico.TipoRecorridoId);

            if (IsFechaTipoUHorarioModificado(recorridoHistorico))
            {
                ValidarHorario(tipoRecorridoDb, recorridoHistorico);

                if ((!tipoRecorridoDb.EsFlexible) && !FechaVisitaEstaDisponible(recorridoHistorico))
                    throw new Exception($"Fecha no disponible.");
            }

            const int completado = 2;

            if (completado == recorridoHistorico.EstadoId)
                recorridoHistorico.FechaCulminacion = DateTime.Now;

            _context.RecorridosHistoricos.Update(recorridoHistorico);

            const int affectedRows = 0;
            bool success = _context.SaveChanges() > affectedRows;

            if (!success)
                throw new Exception("Ocurrio un error al momento de guardar el recorrido historico");

            if (completado == recorridoHistorico.EstadoId)
            {
                await _emailService.SendEmailAsync(new MailRequest
                {
                    From = "danielcleto@presidencia.gob.do",
                    To = recorridoHistorico.Correo,
                    Subject = "Solicitud de Recorrido",
                    Body = "Tu solicitudDto ha sido completada exitoxamente."
                });
            }

            return recorridoHistorico;
        }

        private bool IsFechaTipoUHorarioModificado(RecorridoHistorico recorridoHistorico) 
        {
            return (
                _context.Entry(recorridoHistorico).Property(u => u.HorarioId).IsModified ||
                _context.Entry(recorridoHistorico).Property(u => u.TipoRecorridoId).IsModified ||
                _context.Entry(recorridoHistorico).Property(u => u.FechaVisita).IsModified 
                );
        }

        public bool DeleteAsync(int id)
        {
            var recorrido = _context.RecorridosHistoricos.FirstOrDefault(r => r.Id == id);

            if (recorrido is null)
                throw new NullReferenceException("No se pudo eliminar");

            _context.RecorridosHistoricos.Remove(recorrido);

            return _context.SaveChanges() > 0;
        }

        public bool TipoRecorridoExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RestoreDeleted(int id)
        {
            try
            {
                _context.RecorridosHistoricos.RestoreDeleted(id);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RecorridoHistorico> Deleted()
        {
            return _context.RecorridosHistoricos.IgnoreQueryFilters().Where(t => t.DeletedAt.HasValue).ToList();
        }

        public RecorridoHistorico? GetDeleted(int id)
        {
            return _context.RecorridosHistoricos.IgnoreQueryFilters().FirstOrDefault(t => t.Id == id && t.DeletedAt.HasValue);
        }
    }
}
