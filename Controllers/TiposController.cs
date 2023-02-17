using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdecanesV2.Data;
using EdecanesV2.Models;
using AutoMapper;
using EdecanesV2.Models.DTOs.TipoRecorrido;
using EdecanesV2.Repositories.Abstract;

namespace EdecanesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITiposRepository _tipoRepository;
       // private readonly IHorariosRepository _horariosRepository;
        private readonly IMapper _mapper;

        public TiposRecorridosController(ITiposRecorridosRepository tiposRecorridos, IHorariosRecorridoRepository horariosRecorridoRepository, IMapper mapper)
        {
            _tiposRecorridoRepository = tiposRecorridos;
            _horariosRecorridoRepository = horariosRecorridoRepository;
            _mapper = mapper;
        }


        // GET: TiposRecorridos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoRecorridoReadDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var tipos = await _tiposRecorridoRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TipoRecorridoReadDto>>(tipos));
        }

        // GET: TiposRecorridos/5
        [HttpGet("{id}", Name = "GetAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoRecorrido))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var tipoRecorrido = await _tiposRecorridoRepository.GetByIdAsync(id);
                return Ok(tipoRecorrido);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST: TiposRecorridos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TipoRecorrido))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(NuevoTipoRecorridoDto tipoRecorrido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tipo = await _tiposRecorridoRepository.CreateAsync(_mapper.Map<TipoRecorrido>(tipoRecorrido));
                return CreatedAtRoute(nameof(GetAsync), new { id = tipo.Id }, tipo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: TiposRecorridos
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoRecorrido))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAsync(TipoRecorridoUpdateDto tipoRecorrido)
        {
            var tipo = await _tiposRecorridoRepository.GetByIdAsync(tipoRecorrido.Id);

            if (tipo == null)
                return BadRequest();

            try
            {
                _mapper.Map(tipoRecorrido, tipo);
                var updated = await _tiposRecorridoRepository.EditAsync(tipo);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: TiposRecorridos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _tiposRecorridoRepository.DeleteAsync(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: TiposRecorridos/5/horarios
        [HttpGet("{id}/horarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HorarioDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHorariosByTipoRecorridoIdAsync(int id)
        {
            try
            {
                var horarios = await _horariosRecorridoRepository.GetHorariosByTipoRecorridoIdAsync(id);
                return Ok(_mapper.Map<IEnumerable<HorarioDto>>(horarios));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
