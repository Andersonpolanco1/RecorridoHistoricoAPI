using AutoMapper;
using EdecanesV2.Models.DTOs.FechasManuales;
using EdecanesV2.Models;
using EdecanesV2.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdecanesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FechasManualesController : ControllerBase
    {
        private readonly IFechasManualesRepository _fechasManualesRepository;
        private readonly IMapper _mapper;

        public FechasManualesController(IFechasManualesRepository fechasManualesRepository, IMapper mapper)
        {
            _fechasManualesRepository = fechasManualesRepository;
            _mapper = mapper;
        }

        // GET: FechasManuales
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FechaManual>))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _fechasManualesRepository.GetAllAsync());
        }


        // POST: FechasManuales
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<FechaManual>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(NuevaFechaManualDto fechaManual)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nuevaFecha = await _fechasManualesRepository.CreateAsync(_mapper.Map<FechaManual>(fechaManual));
                return CreatedAtRoute(nameof(GetAsync), new { id = nuevaFecha.Id }, nuevaFecha);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: FechasManuales/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FechaManual))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var fechaManual = await _fechasManualesRepository.GetAsync(id);
                return Ok(fechaManual);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: FechasManuales
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FechaManual))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAsync(ActualizarFechaManualDto fechaManualUpdateDto)
        {
            var fecha = await _fechasManualesRepository.GetAsync(fechaManualUpdateDto.Id);

            if (fecha is null)
                return BadRequest();

            try
            {
                _mapper.Map(fechaManualUpdateDto, fecha);
                var fechaActualizada = await _fechasManualesRepository.EditAsync(fecha);
                return Ok(fechaActualizada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: FechasManuales/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _fechasManualesRepository.DeleteAsync(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
