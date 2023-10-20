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
    public class MedicamentoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        public MedicamentoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitOfwork = unitofwork;
            _mapper = mapper;
        }

        /*
        * MÉTODOS ESPECÍFICOS
        */
        [HttpGet("listarPrecioMayorA/{precio}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicamentoSimpleDto>>> ObtenerPrecioMayorA(int precio)
        {
            Console.WriteLine(precio);
            try{
                var meds = await _unitOfwork.Medicamentos.ObtenerPrecioMayorAAsync(precio);
                return _mapper.Map<List<MedicamentoSimpleDto>>(meds);
                // return Ok(meds);
            }catch(Exception err){
                return NotFound($"No hay registros. \n {err}");
            }
            
        }


        [HttpGet("listarPorLaboratorio/{laboratorio}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicamentoSimpleDto>>> ObtenerPorLab(string laboratorio)
        {
            Console.WriteLine(laboratorio);
            try{
                var meds = await _unitOfwork.Medicamentos.ObtenerMedsXLabAsync(laboratorio);
                return _mapper.Map<List<MedicamentoSimpleDto>>(meds);
                // return Ok(meds);
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
        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetAll()
        {
            var Medicamentos = await _unitOfwork.Medicamentos.GetAllAsync();
            return _mapper.Map<List<MedicamentoDto>>(Medicamentos);
        }
        // PAGINADO
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicamentoDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Medicamentos.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listMedicamento = _mapper.Map<List<MedicamentoDto>>(records);
            return new Pager<MedicamentoDto>(listMedicamento, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }



        // POST
        [HttpPost("crear")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(MedicamentoRegDto model)
        {
            var existingMedicamento = _unitOfwork.Medicamentos.Find(l => l.Nombre == model.Nombre).FirstOrDefault();

            if (existingMedicamento != null)
            {
                return BadRequest("Ya existe un Medicamento con el mismo nombre.");
            }

            var Medicamento = _mapper.Map<Medicamento>(model);
            _unitOfwork.Medicamentos.Add(Medicamento);
            await _unitOfwork.SaveAsync();
            return Ok($"Medicamento creado correctamente!");
        }



        // PUT
        [HttpPut("actualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Put(int id, [FromBody] MedicamentoRegDto MedicamentoActualizado)
        {
            var medicamentoExists = _unitOfwork.Medicamentos.GetByIdAsync(id);

            if (medicamentoExists == null)
            {
                return NotFound();
            }

            var Medicamento = _mapper.Map<Medicamento>(MedicamentoActualizado);
            _unitOfwork.Medicamentos.Update(Medicamento);
            await _unitOfwork.SaveAsync();
            return Ok($"Medicamento {id} actualizado!");
        }



        // DELETE
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var Medicamento = await _unitOfwork.Medicamentos.GetByIdAsync(id);

            if (Medicamento == null)
            {
                return NotFound();
            }

            _unitOfwork.Medicamentos.Remove(Medicamento);
            await _unitOfwork.SaveAsync();
            return Ok($"Registro {id} eliminado correctamente!");
        }
    }
}