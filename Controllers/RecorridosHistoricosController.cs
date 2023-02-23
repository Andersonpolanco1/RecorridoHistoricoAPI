using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico;
using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace RecorridoHistoricoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecorridosHistoricosController : ControllerBase
    {
        private readonly IRecorridosRepository _recorridosRepository;
        private readonly IMapper _mapper;

        public RecorridosHistoricosController(IRecorridosRepository recorridosRepository, IMapper mapper)
        {
            _recorridosRepository = recorridosRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecorridoReadDto>>> GetAll()
        {
            var recorridos = await _recorridosRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RecorridoReadDto>>(recorridos));
        }


        [HttpPost]
        public async Task<ActionResult<RecorridoReadDto>> Create([FromBody] RecorridoCreateDto recorridoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var recorrido = _mapper.Map<RecorridoHistorico>(recorridoDto);
                await _recorridosRepository.CreateAsync(recorrido);

                return CreatedAtAction("GetById", new { recorrido.Id }, _mapper.Map<RecorridoReadDto>(recorrido));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RecorridoHistoricoDetailsDto>> GetById(int id)
        {
            try
            {
                var recorrido = await _recorridosRepository.GetByIdAsync(id);

                if (recorrido == null)
                    return NotFound();

                return Ok(_mapper.Map<RecorridoHistoricoDetailsDto>(recorrido));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecorridoReadDto>> Update(int id,RecorridoUpdateDto recorridoUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != recorridoUpdate.Id)
                return BadRequest();

            try
            {
                var recorrido = await _recorridosRepository.GetByIdAsync(recorridoUpdate.Id);

                if (recorrido == null)
                    return NotFound();

                _mapper.Map(recorridoUpdate, recorrido);
                await _recorridosRepository.EditAsync(recorrido);

                return Ok(_mapper.Map<RecorridoReadDto>(recorrido));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<RecorridoReadDto> Delete(int id)
        {
            try
            {
                var success =  _recorridosRepository.DeleteAsync(id);

                if (!success)
                    return BadRequest();

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("restoredeleted/{id}")]
        public IActionResult RestoreDeleted(int id)
        {
            try
            {
                _recorridosRepository.RestoreDeleted(id);
                return Ok("Restored");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("deleted")]
        public IActionResult Deleted()
        {
            var recorridosEliminados = _recorridosRepository.Deleted();

            return Ok(_mapper.Map<IEnumerable<RecorridoReadDto>>(recorridosEliminados));
        }

        [HttpGet("deleted/{id}")]
        public IActionResult Deleted(int id)
        {
            var recorridoEliminado = _recorridosRepository.GetDeleted(id);

            if (recorridoEliminado == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecorridoReadDto>(recorridoEliminado));
        }
    }
}
