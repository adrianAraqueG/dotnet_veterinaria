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
    public class RazaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public RazaController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<RazaSimpleDto>>> GetAll()
        {
            var Razas = await _unitOfwork.Razas.GetAllAsync();
            return _mapper.Map<List<RazaSimpleDto>>(Razas);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<RazaSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Razas.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listRaza = _mapper.Map<List<RazaSimpleDto>>(records);
            return new Pager<RazaSimpleDto>(listRaza, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(RazaRegDto model)
        {
            var existingRaza = _unitOfwork.Razas.Find(l => l.Nombre == model.Nombre).FirstOrDefault();

            if (existingRaza != null)
            {
                return BadRequest("Ya existe un Raza con el mismo nombre.");
            }

            var Raza = _mapper.Map<Raza>(model);
            _unitOfwork.Razas.Add(Raza);
            await _unitOfwork.SaveAsync();
            return Ok($"Raza creado correctamente!");
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] RazaRegDto RazaActualizado)
        {
            var RazaExists = _unitOfwork.Razas.GetByIdAsync(id);

            if (RazaExists == null)
            {
                return NotFound();
            }

            var Raza = _mapper.Map<Raza>(RazaActualizado);
            _unitOfwork.Razas.Update(Raza);
            await _unitOfwork.SaveAsync();
            return Ok($"Raza {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Raza = await _unitOfwork.Razas.GetByIdAsync(id);

            if (Raza == null)
            {
                return NotFound();
            }

            _unitOfwork.Razas.Remove(Raza);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}