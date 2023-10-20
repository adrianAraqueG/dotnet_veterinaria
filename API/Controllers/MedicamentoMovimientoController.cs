using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]

    [Authorize]
    public class MedicamentoMovimientoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public MedicamentoMovimientoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitOfwork = unitofwork;
            _mapper = mapper;
        }

        /*
        * MÉTODOS ESPECÍFICOS
        */



        /*
        * CRUD
        */
        // GET
        [HttpGet("listar")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicamentoMovimientoSimpleDto>>> GetAll()
        {
            var MedicamentoMovimientos = await _unitOfwork.MedicamentoMovimientos.GetAllAsync();
            return _mapper.Map<List<MedicamentoMovimientoSimpleDto>>(MedicamentoMovimientos);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicamentoMovimientoSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.MedicamentoMovimientos.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listMedicamentoMovimiento = _mapper.Map<List<MedicamentoMovimientoSimpleDto>>(records);
            return new Pager<MedicamentoMovimientoSimpleDto>(listMedicamentoMovimiento, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(MedicamentoMovimientoRegDto model)
        {
            try{
                var MedicamentoMovimiento = _mapper.Map<MedicamentoMovimiento>(model);
                _unitOfwork.MedicamentoMovimientos.Add(MedicamentoMovimiento);
                await _unitOfwork.SaveAsync();
                return Ok($"Movimiento creado correctamente!");
            }catch(Exception err){
                return BadRequest();
            }
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] MedicamentoMovimientoRegDto MedicamentoMovimientoActualizado)
        {
            var MedicamentoMovimientoExists = _unitOfwork.MedicamentoMovimientos.GetByIdAsync(id);

            if (MedicamentoMovimientoExists == null)
            {
                return NotFound();
            }

            var MedicamentoMovimiento = _mapper.Map<MedicamentoMovimiento>(MedicamentoMovimientoActualizado);
            _unitOfwork.MedicamentoMovimientos.Update(MedicamentoMovimiento);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var MedicamentoMovimiento = await _unitOfwork.MedicamentoMovimientos.GetByIdAsync(id);

            if (MedicamentoMovimiento == null)
            {
                return NotFound();
            }

            _unitOfwork.MedicamentoMovimientos.Remove(MedicamentoMovimiento);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}