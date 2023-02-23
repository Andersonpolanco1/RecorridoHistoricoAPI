using RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico;
using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Utils.DataTable;
using Microsoft.AspNetCore.Mvc;

namespace RecorridoHistoricoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardEdecanesRepository _dashboardEdecanesRepository;

        public DashboardController(IDashboardEdecanesRepository dashboardEdecanesRepository)
        {
            _dashboardEdecanesRepository = dashboardEdecanesRepository;
        }

        //POST: api/Dashboard/dt
        [HttpPost("dt")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtResult<RecorridoDataTableDashboard>))]
        public IActionResult GetSolicitudesAsync([FromForm] DtParameters dtParameters)
        {
            var recorridosDT = _dashboardEdecanesRepository.GetSolicitudesDtAsync(dtParameters);
            return Ok(recorridosDT);
        }

        // GET: api/Dashboard/estadisticas/estados
        [HttpGet("estadisticas/estados")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EstadisticaDto>))]
        public async Task<IActionResult> GetEstadisticaEstadosAsync()
        {
            var estadisticas = await _dashboardEdecanesRepository.GetEstadisticaEstadosAsync();
            return Ok(estadisticas);
        }

        // GET: api/Dashboard/estadisticas/tiposrecorrido
        [HttpGet("estadisticas/tiposrecorrido")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EstadisticaDto>))]
        public async Task<IActionResult> GetEstadisticaTiposAsync()
        {
            var estadisticas = await _dashboardEdecanesRepository.GetEstadisticaTiposAsync();
            return Ok(estadisticas);
        }

        // GET: api/Dashboard/cal
        [HttpGet("cal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RecorridoCalendarioDashboardDto>))]
        public async Task<IActionResult> GetRecorridosCalendarioDashboard()
        {
            var recorridos = await _dashboardEdecanesRepository.GetRecorridosCalendarioDashboard();
            return Ok(recorridos);
        }
    }
}
