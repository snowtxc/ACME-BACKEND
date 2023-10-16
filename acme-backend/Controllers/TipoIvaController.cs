using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoIvaController : ControllerBase
    {
        private readonly TipoIvaService _tipoIvaService;

        public TipoIvaController(TipoIvaService tipoIvaService)
        {
            _tipoIvaService = tipoIvaService;
        }

        public IActionResult Index()
        {
            return Ok("");
        }

        [HttpGet]
        public async Task<IActionResult> listarTiposIVA()
        {
            try
            {
                var categorias = await _tipoIvaService.listarTiposIVA();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
