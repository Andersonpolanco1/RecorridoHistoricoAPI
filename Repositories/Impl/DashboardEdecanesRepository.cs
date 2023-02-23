using EdecanesV2.Extensions;
using EdecanesV2.Data;
using EdecanesV2.Models;
using EdecanesV2.Repositories.Abstract;
using EdecanesV2.Utils.DataTable;
using Microsoft.EntityFrameworkCore;
using EdecanesV2.Models.DTOs.RecorridoHistorico;

namespace EdecanesV2.Repositories.Impl
{
    public class DashboardEdecanesRepository : IDashboardEdecanesRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardEdecanesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DtResult<RecorridoDataTableDashboard> GetSolicitudesDtAsync(DtParameters dtParameters)
        {
            var query = _context.RecorridosHistoricos
                .Include(r => r.Estado)
                .Include(r => r.TipoRecorrido)
                .AsQueryable();

            if (!string.IsNullOrEmpty(dtParameters.Search?.Value))
                Filtrar(ref query, dtParameters);

            Ordenar(ref query, dtParameters);

            var filteredResultsCount = query.Count();
            var totalResultsCount = _context.RecorridosHistoricos.Count();

            var data = query
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
                .Select(r => new RecorridoDataTableDashboard
                {
                    Id = r.Id,
                    Nombre = string.Join(" ", r.Nombres, r.Apellidos),
                    FechaCreacion = r.FechaCreacion.ToShortDateString(),
                    FechaVisita = r.FechaVisita.ToShortDateString(),
                    EstadoDescripcion = r.Estado.Nombre,
                    Institucion = r.Institucion,
                    TipoRecorridoDescripcion = r.TipoRecorrido.Descripcion
                })
                .ToList();

            return new DtResult<RecorridoDataTableDashboard>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = data
            };
        }

        private static void Filtrar(ref IQueryable<RecorridoHistorico> query, DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value.ToLower();

            query = query.Where(x =>
                x.Nombres != null && x.Nombres.ToLower().Contains(searchBy.ToLower()) ||
                x.Apellidos != null && x.Apellidos.ToLower().Contains(searchBy) ||
                x.Cedula != null && x.Cedula.ToLower().Contains(searchBy) ||
                x.Correo != null && x.Correo.ToLower().Contains(searchBy) ||
                x.TipoRecorrido.Nombre != null && x.TipoRecorrido.Nombre.ToLower().Contains(searchBy) ||
                x.Institucion != null && x.Institucion.ToLower().Contains(searchBy));

        }

        private static void Ordenar(ref IQueryable<RecorridoHistorico> query, DtParameters dtParameters)
        {
            string orderCriteria;
            bool orderAscendingDirection;

            if (dtParameters.Order == null)
            {
                orderCriteria = "Id";
                orderAscendingDirection = false;
            }
            else
            {
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            if (orderAscendingDirection)
                query = query.OrderByDynamic(orderCriteria, DtOrderDir.Asc);
            else
                query = query.OrderByDynamic(orderCriteria, DtOrderDir.Desc);
        }

        public async Task<IEnumerable<EstadisticaDto>> GetEstadisticaEstadosAsync()
        {
            return await _context.RecorridosHistoricos
                .Include(s => s.Estado)
                .GroupBy(s => s.Estado.Nombre)
                .Select(e => new EstadisticaDto { Id = e.Single().EstadoId, Nombre = e.Key, Cantidad = e.Count() })
                .ToListAsync();
        }
        public async Task<IEnumerable<EstadisticaDto>> GetEstadisticaTiposAsync()
        {
            return await _context.RecorridosHistoricos
                .Include(s => s.TipoRecorrido)
                .GroupBy(s => s.TipoRecorrido.Descripcion)
                .Select(e => new EstadisticaDto { Id = e.Single().TipoRecorridoId, Nombre = e.Key, Cantidad = e.Count() })
                .ToListAsync();
        }

        public async Task<IEnumerable<RecorridoCalendarioDashboardDto>> GetRecorridosCalendarioDashboard()
        {
            return await _context.RecorridosHistoricos.AsNoTracking()
                 .Include(x => x.TipoRecorrido)
                 .Include(x => x.Horario)
                 .Include(x => x.Estado)
                 .Select(r => new RecorridoCalendarioDashboardDto
                 {
                     Id = r.Id,
                     Nombre = string.Join(" ",r.Nombres, r.Apellidos),
                     CantidadVisitantes = r.CantidadVisitantes,
                     FechaVisita = r.FechaVisita.ToShortDateString(),
                     FechaCreacion = r.FechaCreacion.ToShortDateString(),
                     EstadoDescripcion = r.Estado.Nombre,
                     EstadoId = r.EstadoId,
                     Hora = r.Horario.Hora,
                     TipoSolicitudDescripcion = r.TipoRecorrido.Descripcion,
                     TipoSolicitudId = r.TipoRecorrido.Id
                 })
                 .ToListAsync();
        }
    }
}
