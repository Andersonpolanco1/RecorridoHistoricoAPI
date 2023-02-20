﻿using System;
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
using EdecanesV2.Models.DTOs.Horario;

namespace EdecanesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITiposRepository _tiposRepository;
        private readonly IHorariosRepository _horariosRepository;
        private readonly IMapper _mapper;

        public TiposController(ITiposRepository tiposRecorridos, IHorariosRepository horariosRepository, IMapper mapper)
        {
            _tiposRepository = tiposRecorridos;
            _horariosRepository = horariosRepository;
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
                return Ok(tipoRecorrido);
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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAsync(TipoUpdateDto tipoRecorrido)
        {
            var tipo = _tiposRepository.GetByIdAsync(tipoRecorrido.Id).Result;

            if (tipo == null)
                return BadRequest();

            try
            {
                tipo = _mapper.Map(tipoRecorrido, tipo);
                var updated = await _tiposRepository.EditAsync(tipo);
                return Ok(updated);
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
                return Ok();
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
        public async Task<IActionResult> GetHorariosByTipoRecorridoIdAsync(int id)
        {
            try
            {
                var horarios = await _horariosRepository.GetHorariosByTipoRecorridoIdAsync(id);
                return Ok(_mapper.Map<IEnumerable<HorarioDto>>(horarios));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Tipos/5
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

        // GET: api/Tipos/5
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
