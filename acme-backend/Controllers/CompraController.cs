using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Compra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : Controller
    {

        private readonly IBL_Compra _blCompra;
        public CompraController(IBL_Compra blCompra)
        {
            _blCompra = blCompra;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var compra = await _blCompra.getById(id);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listMyCompras")]
        [Authorize(Roles = "Usuario")]

        public async Task<IActionResult> getByCliente()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<CompraDto> compras = await _blCompra.listByCliente(userId);

                return Ok(compras);  
               
            }catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}
