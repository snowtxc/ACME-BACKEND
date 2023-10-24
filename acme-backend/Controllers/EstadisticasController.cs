using acme_backend.Models.Dtos.Ciudad;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadisticasController : ControllerBase
    {
        private readonly EstadisticasService _estadisticaService;

        public EstadisticasController(EstadisticasService estadServic)
        {
            _estadisticaService = estadServic;
        }

        [HttpGet, Route("sort")]
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
    }
}
