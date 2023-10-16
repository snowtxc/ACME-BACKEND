using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService catService)
        {
            _categoriaService = catService;
        }

        public IActionResult Index()
        {
            return Ok("Producto creado correctamente");
        }

        [HttpGet]
        public async Task<IActionResult> listarCategorias()
        {
            try
            {
                var default_Categories = new string[] { };
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var categorias = await _categoriaService.listarCategorias(userId);
                    return Ok(categorias);
                } else
                {
                    return Ok(default_Categories);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
