using BusinessLayer.IBLs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Estado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public CompraController(IBL_Compra blCompra, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _blCompra = blCompra;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [Authorize(Roles = "Usuario,Vendedor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Usuario user = await _userManager.FindByIdAsync(userId);
                var compra = await _blCompra.getById(id);
                if (await _userManager.IsInRoleAsync(user, "Vendedor") && user.EmpresaId != null && compra.Empresa.Id != user.EmpresaId) { 
         
                    return NotFound("Compra no encontrada");

                }

                if (await _userManager.IsInRoleAsync(user, "Usuario") && compra.Comprador.Id != user.Id) { 
                    return NotFound("Compra no encontrada");
                }


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

        [HttpPost("{id}/cambiarEstado")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> cambiarEstado(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Usuario user = await _userManager.FindByIdAsync(userId);
                var compra = await _blCompra.getById(id);
                if (user.EmpresaId != null && compra.Empresa.Id != user.EmpresaId)
                {
                    return NotFound("Compra no encontrada");

                }
                EstadoCompraDto nuevoEstado = await _blCompra.pasarAlSiguienteEstado(compra.Id);
                return Ok(nuevoEstado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }










    }
}
