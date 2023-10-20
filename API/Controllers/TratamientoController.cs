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

    // [Authorize]
    public class TratamientoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public TratamientoController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<TratamientoSimpleDto>>> GetAll()
        {
            var Tratamientos = await _unitOfwork.Tratamientos.GetAllAsync();
            return _mapper.Map<List<TratamientoSimpleDto>>(Tratamientos);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TratamientoSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Tratamientos.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listTratamiento = _mapper.Map<List<TratamientoSimpleDto>>(records);
            return new Pager<TratamientoSimpleDto>(listTratamiento, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(TratamientoRegDto model)
        {
            try{
                var Tratamiento = _mapper.Map<Tratamiento>(model);
                _unitOfwork.Tratamientos.Add(Tratamiento);
                await _unitOfwork.SaveAsync();
                return Ok($"Tratamiento creado correctamente!");
            }catch(Exception err){
                return BadRequest();
            }
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] TratamientoRegDto TratamientoActualizado)
        {
            var TratamientoExists = _unitOfwork.Tratamientos.GetByIdAsync(id);

            if (TratamientoExists == null)
            {
                return NotFound();
            }

            var Tratamiento = _mapper.Map<Tratamiento>(TratamientoActualizado);
            _unitOfwork.Tratamientos.Update(Tratamiento);
            await _unitOfwork.SaveAsync();
            return Ok($"Tratamiento {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Tratamiento = await _unitOfwork.Tratamientos.GetByIdAsync(id);

            if (Tratamiento == null)
            {
                return NotFound();
            }

            _unitOfwork.Tratamientos.Remove(Tratamiento);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}