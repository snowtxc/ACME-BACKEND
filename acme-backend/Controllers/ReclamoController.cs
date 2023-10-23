using acme_backend.Models.Dtos.Reclamo;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReclamoController : Controller
    {

        private readonly ReclamoService _reclamoService;

        public ReclamoController(ReclamoService reclamoService)
        {
            _reclamoService = reclamoService;
        }


        [HttpGet]
        public async Task<IActionResult> listar()
        {
            try
            {
                List<ReclamoDto> reclamos  = await _reclamoService.list();
                return Ok(reclamos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
