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
    public class CitaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public CitaController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetAll()
        {
            var Citas = await _unitOfwork.Citas.GetAllAsync();
            return _mapper.Map<List<CitaDto>>(Citas);
        }

        [HttpGet("listar")]
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<CitaDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Citas.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listCita = _mapper.Map<List<CitaDto>>(records);
            return new Pager<CitaDto>(listCita, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(CitaRegDto model)
        {
            var cita = _mapper.Map<Cita>(model);
            var result = await _unitOfwork.Citas.RegisterAsync(cita);
            return Ok(result);
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] CitaRegDto citaActualizada)
        {
            var citaExists = _unitOfwork.Citas.GetByIdAsync(id);

            if (citaExists == null)
            {
                return NotFound();
            }

            var cita = _mapper.Map<Cita>(citaActualizada);
            _unitOfwork.Citas.Update(cita);
            await _unitOfwork.SaveAsync();
            return Ok($"Cita ${id} actualizada!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var cita = await _unitOfwork.Citas.GetByIdAsync(id);

            if (cita == null)
            {
                return NotFound();
            }

            _unitOfwork.Citas.Remove(cita);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro ${id} eliminado correctamente!");
        }
    }
}