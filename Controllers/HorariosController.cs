using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.Horario;
using EdecanesV2.Repositories.Abstract;
using EdecanesV2.Repositories.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdecanesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosRecorridosController : ControllerBase
    {
        private readonly IHorariosRepository _horariosRepository;
        private readonly IMapper _mapper;

        public HorariosRecorridosController(IHorariosRepository repo, IMapper mapper)
        {
            _horariosRepository = repo;
            _mapper = mapper;
        }

        // GET: api/Horarios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HorarioDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var horarios = await _horariosRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<HorarioDto>>(horarios));
        }

        // GET: api/Horarios/3
        [HttpGet("{horarioId}", Name = "GetByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HorarioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int horarioId)
        {
            try
            {
                var horario = await _horariosRepository.GetByIdAsync(horarioId);

                return Ok(_mapper.Map<HorarioDto>(horario));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST: api/Horarios
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HorarioDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(HorarioCreateDto horarioRecorrido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var horario = _mapper.Map<Horario>(horarioRecorrido);
                await _horariosRepository.CreateAsync(horario);
                return CreatedAtRoute(nameof(GetAsync), new { id = horario.Id }, _mapper.Map<HorarioDto>(horario));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: api/Horarios
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HorarioDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAsync(HorarioUpdateDto horarioRecorrido)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var horarioDb = _horariosRepository.GetByIdAsync(horarioRecorrido.Id).Result;
            
                if (horarioDb == null)
                    return BadRequest();

                _mapper.Map(horarioRecorrido, horarioDb);
                var horario = await _horariosRepository.EditAsync(horarioDb);
                return Ok(_mapper.Map<HorarioDto>(horario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Delete: api/Horarios/3
        [HttpDelete("{horarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int horarioId)
        {
            try
            {
                await _horariosRepository.DeleteAsync(horarioId);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Horarios/3/TipoRecorrido
        [HttpGet("{horarioId}/TipoRecorrido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTiposRecorridos(int horarioId)
        {
            try
            {
                return Ok(await _horariosRepository.GetTipoRecorridoDeHorario(horarioId));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/tandas/1/tipos/4/horarios
        [HttpGet("/api/tandas/{tandaId}/tipos/{tiporecorridoId}/horarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHorariosByTipoRecorrido(int tandaId, int tiporecorridoId)
        {
            var horarios = await _horariosRepository.GetHorariosByTipoRecorrido(tandaId, tiporecorridoId);
            return Ok(_mapper.Map<IEnumerable<HorarioDto>>(horarios));

        }



        // POST: api/horarios/5
        [HttpPost("restoredeleted/{id}")]
        public IActionResult RestoreDeleted(int id)
        {
            try
            {
                _horariosRepository.RestoreDeleted(id);
                return Ok("Restored");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/horarios/5
        [HttpGet("deleted")]
        public IActionResult Deleted()
        {
            return Ok(_horariosRepository.Deleted());
        }

        // GET: api/horarios/deleted/5
        [HttpGet("deleted/{id}")]
        public IActionResult Deleted(int id)
        {
            var horario = _horariosRepository.GetDeleted(id);

            if (horario == null)
            {
                return NotFound();
            }

            return Ok(horario);
        }
    }
}
