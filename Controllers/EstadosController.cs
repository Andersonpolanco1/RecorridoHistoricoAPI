using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdecanesV2.Data;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.EstadoDtos;
using AutoMapper;
using EdecanesV2.Extensions;

namespace EdecanesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EstadosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await _context.Estados.ToListAsync();
        }

        // GET: api/Estados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDto>> GetEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return _mapper.Map<EstadoDto>(estado);
        }

        // PUT: api/Estados/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EstadoDto>> PutEstado(int id, EstadoDto estadoDto)
        {
            if (id != estadoDto.Id)
                return BadRequest();

            var estado = await _context.Estados.FindAsync(id);

            if(estado is null)
                return BadRequest();

            _mapper.Map(estadoDto, estado);
            _context.Estados.Update(estado);
            _context.SaveChanges();

            return Ok(_mapper.Map<EstadoDto>(estado));

        }

        // POST: api/Estados
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(EstadoCreateDto estadoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estado = _mapper.Map<Estado>(estadoDto);

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.Id }, _mapper.Map<EstadoDto>(estado));
        }

        // DELETE: api/Estados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        // POST: api/Estados/restoredeleted/5
        [HttpPost("restoredeleted/{id}")]
        public IActionResult RestoreDeleted(int id)
        {
            try
            {
                _context.Estados.RestoreDeleted(id);
                _context.SaveChanges();
                return Ok("Restored");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Estados/estadoEliminado
        [HttpGet("Eliminados")]
        public IActionResult Deleted()
        {
            var estadosEliminados =_context.Estados.IgnoreQueryFilters().Where(e => e.DeletedAt.HasValue).ToList();
            return Ok(estadosEliminados);
        }

        // GET: api/Estados/estadoEliminado/5
        [HttpGet("Eliminados/{id}")]
        public IActionResult Deleted(int id)
        {
            var estadoEliminado = _context.Estados.IgnoreQueryFilters().FirstOrDefault(t => t.Id == id && t.DeletedAt.HasValue);

            if (estadoEliminado == null)
            {
                return NotFound();
            }

            return Ok(estadoEliminado);
        }
    }
}
