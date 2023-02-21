using AutoMapper.Internal;
using AutoMapper;
using EdecanesV2.Data;
using EdecanesV2.Repositories.Abstract;
using EdecanesV2.Services.Abstract;
using EdecanesV2.Extensions;
using EdecanesV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualBasic;
using EdecanesV2.Utils;
using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Repositories.Impl
{
    public class RecorridoRepository : IRecorridosRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public RecorridoRepository(ApplicationDbContext context, IEmailService emailService, IMapper mapper)
        {
            _context = context;
            _emailService = emailService;
            _mapper = mapper;
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
                throw new Exception($"Horario no disponible.");

            const int creado = 1;
            recorridoHistorico.EstadoId = creado;
            recorridoHistorico.FechaCreacion = DateTime.Now;
            _context.RecorridosHistoricos.Add(recorridoHistorico);

            const int affectedRows = 0;
            bool success = _context.SaveChanges() > affectedRows;

            if (!success)
                throw new Exception("Ocurrio un error al momento de guardar el recorrido historico");

            await _emailService.SendEmailRecorridoCreadoAsync(recorridoHistorico);
            return recorridoHistorico;
        }

        private void ValidarHorario(Tipo? tipoRecorridoDb, RecorridoHistorico newRecorrido)
        {
            if (tipoRecorridoDb == null)
                throw new Exception("Tipo de recorrido no valido");

            if (!tipoRecorridoDb.Horarios.Any(h => h.Id == newRecorrido.HorarioId))
                throw new Exception($"Tipo de recorrido {tipoRecorridoDb.Descripcion} no tiene asignado el horario seleccionado.");


            var dia = DateAndTimeUtils.ToEnumDiaSemana(newRecorrido.FechaVisita);

            if (!tipoRecorridoDb.Horarios.Any(h => h.Dia == dia))
                throw new Exception("Tipo de recorrido u horario no valido");

            if (!tipoRecorridoDb.Horarios.Any(h => h.Id == newRecorrido.HorarioId))
                throw new Exception($"Tipo de recorrido {tipoRecorridoDb.Descripcion} no disponible el dia {dia}.");

        }

        private bool FechaVisitaEstaDisponible(RecorridoHistorico recorrido)
        {
            var recorridosProgramados = GetRecorridosProgramados(recorrido.FechaVisita);

            return !recorridosProgramados.Any(rp => rp.HorarioId == recorrido.HorarioId);
        }

        private List<RecorridoHistorico> GetRecorridosProgramados(DateTime fechaVisita)
        {
            return _context.RecorridosHistoricos.AsNoTracking()
                .Include(r => r.Horario)
                .Include(r => r.TipoRecorrido)
                .Where(r => r.FechaVisita == fechaVisita).ToList();
        }

        public Task<RecorridoHistorico> EditAsync(RecorridoHistorico recorridoHistorico)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
