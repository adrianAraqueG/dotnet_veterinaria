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
    public class PropietarioController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public PropietarioController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitOfwork = unitofwork;
            _mapper = mapper;
        }

        /*
        * MÉTODOS ESPECÍFICOS
        */
        [HttpGet("listarPropsConMascotas")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MascotaSimpleDto>>> PropietariosConMascotas()
        {
            try{
                var props = await _unitOfwork.Propietarios.ObtenerPropsConMasc();
                // return _mapper.Map<List<MascotaSimpleDto>>(masc);
                return Ok(props);
            }catch(Exception err){
                return NotFound($"No hay registros. \n {err}");
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
        public async Task<ActionResult<IEnumerable<PropietarioSimpleDto>>> GetAll()
        {
            var Propietarios = await _unitOfwork.Propietarios.GetAllAsync();
            return _mapper.Map<List<PropietarioSimpleDto>>(Propietarios);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PropietarioSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Propietarios.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listPropietario = _mapper.Map<List<PropietarioSimpleDto>>(records);
            return new Pager<PropietarioSimpleDto>(listPropietario, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(PropietarioRegDto model)
        {
            try{
                var Propietario = _mapper.Map<Propietario>(model);
                _unitOfwork.Propietarios.Add(Propietario);
                await _unitOfwork.SaveAsync();
                return Ok($"Propietario creado correctamente!");
            }catch(Exception err){
                return BadRequest();
            }
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] PropietarioRegDto PropietarioActualizado)
        {
            var PropietarioExists = _unitOfwork.Propietarios.GetByIdAsync(id);

            if (PropietarioExists == null)
            {
                return NotFound();
            }

            var Propietario = _mapper.Map<Propietario>(PropietarioActualizado);
            _unitOfwork.Propietarios.Update(Propietario);
            await _unitOfwork.SaveAsync();
            return Ok($"Propietario {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Propietario = await _unitOfwork.Propietarios.GetByIdAsync(id);

            if (Propietario == null)
            {
                return NotFound();
            }

            _unitOfwork.Propietarios.Remove(Propietario);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}