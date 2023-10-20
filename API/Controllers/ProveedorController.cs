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
    public class ProveedorController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public ProveedorController(IUnitOfWork unitofwork, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<ProveedorSimpleDto>>> GetAll()
        {
            var Proveedors = await _unitOfwork.Proveedores.GetAllAsync();
            return _mapper.Map<List<ProveedorSimpleDto>>(Proveedors);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProveedorSimpleDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Proveedores.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listProveedor = _mapper.Map<List<ProveedorSimpleDto>>(records);
            return new Pager<ProveedorSimpleDto>(listProveedor, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(ProveedorRegDto model)
        {
            var existingProveedor = _unitOfwork.Proveedores.Find(l => l.Nombre == model.Nombre).FirstOrDefault();

            if (existingProveedor != null)
            {
                return BadRequest("Ya existe un Proveedor con el mismo nombre.");
            }

            var Proveedor = _mapper.Map<Proveedor>(model);
            _unitOfwork.Proveedores.Add(Proveedor);
            await _unitOfwork.SaveAsync();
            return Ok($"Proveedor creado correctamente!");
            
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] ProveedorRegDto ProveedorActualizado)
        {
            var ProveedorExists = _unitOfwork.Proveedores.GetByIdAsync(id);

            if (ProveedorExists == null)
            {
                return NotFound();
            }

            var Proveedor = _mapper.Map<Proveedor>(ProveedorActualizado);
            _unitOfwork.Proveedores.Update(Proveedor);
            await _unitOfwork.SaveAsync();
            return Ok($"Proveedor {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Proveedor = await _unitOfwork.Proveedores.GetByIdAsync(id);

            if (Proveedor == null)
            {
                return NotFound();
            }

            _unitOfwork.Proveedores.Remove(Proveedor);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}