using AutoMapper;
using RecorridoHistoricoApi.Models.DTOs.Horarios;
using RecorridoHistoricoApi.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecorridoHistoricoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadesController : ControllerBase
    {
        private readonly IDisponibilidadesRepository _disponibilidades;
        private readonly IMapper _mapper;

        public DisponibilidadesController(IDisponibilidadesRepository disponibilidades, IMapper mapper)
        {
            _disponibilidades = disponibilidades;
            _mapper = mapper;
        }

        [HttpGet("FechasNoDisponibles")]
        public ActionResult FechasNoDisponibles()
        {
            return Ok(_disponibilidades.FechasNoDisponibles());
        }

        [HttpGet("horarios")]
        public ActionResult HorariosDisponibles(DateTime fecha)
        {
            var horarios = _disponibilidades.HorariosDisponibles(fecha);
            return Ok(_mapper.Map<IEnumerable<HorarioDto>>(horarios));
        }

        [HttpGet("TiposRecorrido")]
        public ActionResult TiposRecorridoDisponibles(DateTime fecha)
        {
            var tipos = _disponibilidades.TiposRecorridoDisponibles(fecha);
            return Ok(tipos);
        }
    }
}
