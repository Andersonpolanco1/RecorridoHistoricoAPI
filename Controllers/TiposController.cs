using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecorridoHistoricoApi.Data;
using RecorridoHistoricoApi.Models;
using AutoMapper;
using RecorridoHistoricoApi.Models.DTOs.TipoRecorrido;
using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Models.DTOs.Horarios;

namespace RecorridoHistoricoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITiposRepository _tiposRepository;
        private readonly IMapper _mapper;

        public TiposController(ITiposRepository tiposRecorridos, IMapper mapper)
        {
            _tiposRepository = tiposRecorridos;
            _mapper = mapper;
        }


        // GET: api/Tipos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoReadDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var tipos = await _tiposRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TipoReadDto>>(tipos));
        }

        // GET: api/Tipos/5
        [HttpGet("{id}", Name = "GetAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tipo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var tipoRecorrido = await _tiposRepository.GetByIdAsync(id);
                return Ok(_mapper.Map<TipoReadDto>(tipoRecorrido));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST: api/Tipos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TipoReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(TipoCreateDto tipoRecorrido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tipo = await _tiposRepository.CreateAsync(_mapper.Map<Tipo>(tipoRecorrido));
                return CreatedAtRoute(nameof(GetAsync), new { id = tipo.Id }, _mapper.Map<TipoReadDto>(tipo));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: api/Tipos
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAsync(int id,TipoUpdateDto tipoRecorrido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != tipoRecorrido.Id)
                return BadRequest();

            var tipo = _tiposRepository.GetByIdAsync(tipoRecorrido.Id).Result;

            if (tipo == null)
                return BadRequest();

            try
            {
                tipo = _mapper.Map(tipoRecorrido, tipo);
                var updated = await _tiposRepository.EditAsync(tipo);
                return Ok(_mapper.Map<TipoReadDto>(updated));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Tipos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _tiposRepository.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Tipos/5/horarios
        [HttpGet("{id}/horarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HorarioDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHorariosByTipoRecorridoIdAsync(int id)
        {
            try
            {
                var horarios =  _tiposRepository.GetHorarios(id);
                return Ok(_mapper.Map<IEnumerable<HorarioDto>>(horarios));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Tipos/5/horarios/6
        [HttpPost("{id}/horarios/{horarioId}")]
        public IActionResult AgregarHorario(int id, int horarioId)
        {
            try
            {
                _tiposRepository.AddHorario(id, horarioId);
                return Ok("horario agregado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Tipos/5/horarios/6
        [HttpDelete("{id}/horarios/{horarioId}")]
        public IActionResult RemoverHorario(int id, int horarioId)
        {
            try
            {
                _tiposRepository.RemoveHorario(id, horarioId);
                return Ok("horario removido");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Tipos/restoredeleted/5
        [HttpPost("restoredeleted/{id}")]
        public IActionResult RestoreDeleted(int id)
        {
            try
            {
                _tiposRepository.RestoreDeleted(id);
                return Ok("Restored");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Tipos/deleted
        [HttpGet("deleted")]
        public IActionResult Deleted()
        {
            return Ok(_tiposRepository.Deleted());
        }

        // GET: api/Tipos/deleted/5
        [HttpGet("deleted/{id}")]
        public IActionResult Deleted(int id)
        {
            var tipo = _tiposRepository.GetDeleted(id);

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }
    }
}
