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
    public class TipoMovimientoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public TipoMovimientoController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<TipoMovimientoSimpleDto>>> GetAll()
        {
            var TipoMovimientos = await _unitOfwork.TipoMovimientos.GetAllAsync();
            return _mapper.Map<List<TipoMovimientoSimpleDto>>(TipoMovimientos);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TipoMovimientoSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.TipoMovimientos.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listTipoMovimiento = _mapper.Map<List<TipoMovimientoSimpleDto>>(records);
            return new Pager<TipoMovimientoSimpleDto>(listTipoMovimiento, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(TipoMovimientoRegDto model)
        {
            var existingTipoMovimiento = _unitOfwork.TipoMovimientos.Find(l => l.Nombre == model.Nombre).FirstOrDefault();

            if (existingTipoMovimiento != null)
            {
                return BadRequest("Ya existe un TipoMovimiento con el mismo nombre.");
            }

            var TipoMovimiento = _mapper.Map<TipoMovimiento>(model);
            _unitOfwork.TipoMovimientos.Add(TipoMovimiento);
            await _unitOfwork.SaveAsync();
            return Ok($"TipoMovimiento creado correctamente!");
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] TipoMovimientoRegDto TipoMovimientoActualizado)
        {
            var TipoMovimientoExists = _unitOfwork.TipoMovimientos.GetByIdAsync(id);

            if (TipoMovimientoExists == null)
            {
                return NotFound();
            }

            var TipoMovimiento = _mapper.Map<TipoMovimiento>(TipoMovimientoActualizado);
            _unitOfwork.TipoMovimientos.Update(TipoMovimiento);
            await _unitOfwork.SaveAsync();
            return Ok($"TipoMovimiento {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var TipoMovimiento = await _unitOfwork.TipoMovimientos.GetByIdAsync(id);

            if (TipoMovimiento == null)
            {
                return NotFound();
            }

            _unitOfwork.TipoMovimientos.Remove(TipoMovimiento);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}