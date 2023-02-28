using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Utils.DataTable;
using Microsoft.AspNetCore.Mvc;
using RecorridoHistoricoApi.Models.DTOs.Dashboard;
using RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico;
using RecorridoHistoricoApi.Repositories.Impl;
using AutoMapper;

namespace RecorridoHistoricoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardEdecanesRepository _dashboardEdecanesRepository;
        private readonly IRecorridosRepository _recorridosRepository;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardEdecanesRepository dashboardEdecanesRepository, IRecorridosRepository recorridosRepository, IMapper mapper)
        {
            _dashboardEdecanesRepository = dashboardEdecanesRepository;
            _recorridosRepository = recorridosRepository;
            _mapper = mapper;
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


        // PUT: api/Dashboard/recorridoshistoricos
        [HttpPut("recorridoshistoricos/{recorridoId}")]
        public async Task<IActionResult> ActualizarRecorrido(int recorridoId, RecorridoDashboardUpdateDto recorridoDashboardUpdate)
        {
            if (recorridoId != recorridoDashboardUpdate.Id)
                return BadRequest();

            try
            {
                var recorrido = await _recorridosRepository.GetByIdAsync(recorridoDashboardUpdate.Id);

                if (recorrido == null)
                    return NotFound();

                _mapper.Map(recorridoDashboardUpdate, recorrido);
                _dashboardEdecanesRepository.ActualizarRecorrido(recorrido);

                return Ok(_mapper.Map<RecorridoReadDto>(recorrido));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
