using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadisticasController : ControllerBase
    {
        private readonly IBL_Empresa _empresaService;
        private readonly IBL_Estadisticas _estadisticaService;

        public EstadisticasController(IBL_Estadisticas estadServic, IBL_Empresa empresaService)
        {
            _estadisticaService = estadServic;
            _empresaService = empresaService;
        }

        [HttpGet, Route("sort")]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> listarEstadisticas()
        {
            try
            {
                var ciudades = await _estadisticaService.listarEstadisticas();
                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> listarEstadisticasAdmin()
        {
            try
            {
                var stats = await _estadisticaService.listarEstadisticasAdmin();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("empresa")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> getVentasByEmpresa()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                EmpresaDto empresa = await _empresaService.getByUser(userId);
                EmpresaEstadisticasDTO stats = await this._estadisticaService.listarEstadisticasEmpresa(empresa.Id);
                return Ok(stats);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
