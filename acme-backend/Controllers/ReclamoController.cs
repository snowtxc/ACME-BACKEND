using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Reclamo;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReclamoController : Controller
    {

        private readonly IBL_Reclamo _reclamoService;

        public ReclamoController(IBL_Reclamo reclamoService)
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
