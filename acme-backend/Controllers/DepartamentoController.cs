using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Ciudad;
using DataAccessLayer.Models.Dtos.Departamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IBL_Departamento _departamentoService;
        private readonly IBL_Ciudad _ciudadService;

        public DepartamentoController(IBL_Departamento deptoService, IBL_Ciudad ciudadService)
        {
            _departamentoService = deptoService;
            _ciudadService = ciudadService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> listarDepartamentos()
        {
            try
            {
                var departamentos = await _departamentoService.listarDepartamentos();
                return Ok(departamentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> getDepartamentoById(int id)
        {
            try
            {
                var departamento = await _departamentoService.getDepartamentoById(id);
                return Ok(departamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> createDepartamento(DepartamentoCreateDTO depto)
        {
            try
            {
                var createdDepto = await _departamentoService.createDepartamento(depto);
                return CreatedAtAction(nameof(getDepartamentoById), new { id = createdDepto.Id }, createdDepto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateDepartamento(DepartamentoEditDTO departamento)
        {
            try
            {
                var updatedDepartamento = await _departamentoService.updateDepartamento(departamento);
                return Ok(updatedDepartamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteDepartamento(int id)
        {
            try
            {
                await _departamentoService.deleteDepartamento(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/ciudades")]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> getCiudadesByDepartamento(int id)
        {
            try
            {
                List<CiudadDTO> ciudades  = await _ciudadService.listarCiudadesByDepartamento(id);
                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
