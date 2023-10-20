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
    public class EspecieController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public EspecieController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<EspecieSimpleDto>>> GetAll()
        {
            var Especies = await _unitOfwork.Especies.GetAllAsync();
            return _mapper.Map<List<EspecieSimpleDto>>(Especies);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<EspecieSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Especies.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listEspecie = _mapper.Map<List<EspecieSimpleDto>>(records);
            return new Pager<EspecieSimpleDto>(listEspecie, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(EspecieRegDto model)
        {
            var existingEspecie = _unitOfwork.Especies.Find(l => l.Nombre == model.Nombre).FirstOrDefault();

            if (existingEspecie != null)
            {
                return BadRequest("Ya existe un Especie con el mismo nombre.");
            }

            var Especie = _mapper.Map<Especie>(model);
            _unitOfwork.Especies.Add(Especie);
            await _unitOfwork.SaveAsync();
            return Ok($"Especie creado correctamente!");
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] EspecieRegDto EspecieActualizado)
        {
            var EspecieExists = _unitOfwork.Especies.GetByIdAsync(id);

            if (EspecieExists == null)
            {
                return NotFound();
            }

            var Especie = _mapper.Map<Especie>(EspecieActualizado);
            _unitOfwork.Especies.Update(Especie);
            await _unitOfwork.SaveAsync();
            return Ok($"Especie {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Especie = await _unitOfwork.Especies.GetByIdAsync(id);

            if (Especie == null)
            {
                return NotFound();
            }

            _unitOfwork.Especies.Remove(Especie);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}