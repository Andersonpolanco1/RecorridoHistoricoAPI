using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.RecorridoHistorico;
using EdecanesV2.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EdecanesV2.Controllers
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
        public async Task<ActionResult<IEnumerable<RecorridoHistorico>>> GetAll()
        {
            return Ok(await _recorridosRepository.GetAllAsync());
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<RecorridoHistorico>>> Create([FromBody] RecorridoCreateDto recorridoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var recorrido = _mapper.Map<RecorridoHistorico>(recorridoDto);
                await _recorridosRepository.CreateAsync(recorrido);
                return Ok(recorrido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
