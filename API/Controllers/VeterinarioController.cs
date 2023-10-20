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
    public class VeterinarioController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public VeterinarioController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitOfwork = unitofwork;
            _mapper = mapper;
        }

        /*
        * MÉTODOS ESPECÍFICOS
        */
        [HttpGet("listarCirujanosCardiovasculares")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VeterinarioSimpleDto>>> GetCirujanosCV(){
            var cirujanos = await _unitOfwork.Veterinarios.ObtenerTodosCirujanosCVAsync();
            if(cirujanos != null){
                return _mapper.Map<List<VeterinarioSimpleDto>>(cirujanos);
            }else{
                return NotFound();
            }
        }



        /*
        * CRUD
        */
        // GET
        [HttpGet("listar")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VeterinarioSimpleDto>>> GetAll()
        {
            var Veterinarios = await _unitOfwork.Veterinarios.GetAllAsync();
            return _mapper.Map<List<VeterinarioSimpleDto>>(Veterinarios);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VeterinarioSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Veterinarios.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listVeterinario = _mapper.Map<List<VeterinarioSimpleDto>>(records);
            return new Pager<VeterinarioSimpleDto>(listVeterinario, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(VeterinarioRegDto model)
        {
            try{
                var Veterinario = _mapper.Map<Veterinario>(model);
                _unitOfwork.Veterinarios.Add(Veterinario);
                await _unitOfwork.SaveAsync();
                return Ok($"Veterinario creado correctamente!");
            }catch(Exception err){
                return BadRequest();
            }
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] VeterinarioRegDto VeterinarioActualizado)
        {
            var VeterinarioExists = _unitOfwork.Veterinarios.GetByIdAsync(id);

            if (VeterinarioExists == null)
            {
                return NotFound();
            }

            var Veterinario = _mapper.Map<Veterinario>(VeterinarioActualizado);
            _unitOfwork.Veterinarios.Update(Veterinario);
            await _unitOfwork.SaveAsync();
            return Ok($"Veterinario {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Veterinario = await _unitOfwork.Veterinarios.GetByIdAsync(id);

            if (Veterinario == null)
            {
                return NotFound();
            }

            _unitOfwork.Veterinarios.Remove(Veterinario);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}