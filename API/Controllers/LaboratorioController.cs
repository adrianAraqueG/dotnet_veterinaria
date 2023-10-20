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
    public class LaboratorioController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public LaboratorioController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<LaboratorioDto>>> GetAll()
        {
            var Laboratorios = await _unitOfwork.Laboratorios.GetAllAsync();
            return _mapper.Map<List<LaboratorioDto>>(Laboratorios);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<LaboratorioDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Laboratorios.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listLaboratorio = _mapper.Map<List<LaboratorioDto>>(records);
            return new Pager<LaboratorioDto>(listLaboratorio, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(LaboratorioRegDto model)
        {
            var existingLaboratorio = _unitOfwork.Laboratorios.Find(l => l.Nombre == model.Nombre).FirstOrDefault();

            if (existingLaboratorio != null)
            {
                return BadRequest("Ya existe un laboratorio con el mismo nombre.");
            }

            var Laboratorio = _mapper.Map<Laboratorio>(model);
            _unitOfwork.Laboratorios.Add(Laboratorio);
            await _unitOfwork.SaveAsync();
            return Ok(Laboratorio);
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] LaboratorioRegDto LaboratorioActualizado)
        {
            var laboratorioExists = _unitOfwork.Laboratorios.GetByIdAsync(id);

            if (laboratorioExists == null)
            {
                return NotFound();
            }

            var Laboratorio = _mapper.Map<Laboratorio>(LaboratorioActualizado);
            _unitOfwork.Laboratorios.Update(Laboratorio);
            await _unitOfwork.SaveAsync();
            return Ok($"Laboratorio {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Laboratorio = await _unitOfwork.Laboratorios.GetByIdAsync(id);

            if (Laboratorio == null)
            {
                return NotFound();
            }

            _unitOfwork.Laboratorios.Remove(Laboratorio);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}