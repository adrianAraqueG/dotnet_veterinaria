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
    public class MascotaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public MascotaController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitOfwork = unitofwork;
            _mapper = mapper;
        }

        /*
        * MÉTODOS ESPECÍFICOS
        */
        [HttpGet("listarPorEspecie/{especie}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MascotaSimpleDto>>> ObtenerPorEspecie(string especie)
        {
            try{
                var masc = await _unitOfwork.Mascotas.ObtenerPorEspecie(especie);
                return _mapper.Map<List<MascotaSimpleDto>>(masc);
                // return Ok(masc);
            }catch(Exception err){
                return NotFound($"No hay registros. \n {err}");
            }
        }

        [HttpGet("listarAgrupacionPorEspecie")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerAgrupacionXEspecie()
        {
            try{
                var masc = await _unitOfwork.Mascotas.ObtenerAgrupadasPorEspecie();
                return Ok(masc);
            }catch(Exception err){
                return NotFound($"No hay registros. \n {err}");
            }
        }

        [HttpGet("listarXVeterinario/{idVet}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerXVet(int IdVet)
        {
            try{
                var masc = await _unitOfwork.Mascotas.ObtenerMascXVeterinario(IdVet);
                return _mapper.Map<List<MascotaSimpleDto>>(masc);
                // return Ok(masc);
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
        public async Task<ActionResult<IEnumerable<MascotaSimpleDto>>> GetAll()
        {
            var Mascotas = await _unitOfwork.Mascotas.GetAllAsync();
            return _mapper.Map<List<MascotaSimpleDto>>(Mascotas);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MascotaSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Mascotas.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listMascota = _mapper.Map<List<MascotaSimpleDto>>(records);
            return new Pager<MascotaSimpleDto>(listMascota, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(MascotaRegDto model)
        {
            try{
                var Mascota = _mapper.Map<Mascota>(model);
                _unitOfwork.Mascotas.Add(Mascota);
                await _unitOfwork.SaveAsync();
                return Ok($"Mascota creado correctamente!");
            }catch(Exception err){
                return BadRequest();
            }
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] MascotaRegDto MascotaActualizado)
        {
            var MascotaExists = _unitOfwork.Mascotas.GetByIdAsync(id);

            if (MascotaExists == null)
            {
                return NotFound();
            }

            var Mascota = _mapper.Map<Mascota>(MascotaActualizado);
            _unitOfwork.Mascotas.Update(Mascota);
            await _unitOfwork.SaveAsync();
            return Ok($"Mascota {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Mascota = await _unitOfwork.Mascotas.GetByIdAsync(id);

            if (Mascota == null)
            {
                return NotFound();
            }

            _unitOfwork.Mascotas.Remove(Mascota);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}