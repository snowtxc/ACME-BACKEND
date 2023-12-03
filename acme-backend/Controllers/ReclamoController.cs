using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Reclamo;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> crearReclamo(ReclamoCreateDTO data)
        {
            try
            {
                await _reclamoService.crearReclamo(data);
                return Ok(new OkDTO
                {
                    ok = true,
                    message = "Reclamo creado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut, Route("cerrar-reclamo/{reclamoId}")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> cerrarReclamo(int reclamoId)
        {
            try
            {
                await _reclamoService.cerrarReclamo(reclamoId);
                return Ok(new OkDTO
                {
                    ok = true,
                    message = "Reclamo cerrado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
