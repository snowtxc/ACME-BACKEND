using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productService;

        public ProductoController(ProductoService prodService)
        {
            _productService = prodService;
        }

        public IActionResult Index()
        {
            return Ok("Producto creado correctamente");
        }

        [HttpPost]
        public async Task<IActionResult> crearProducto(CrearProductoDTO data)
        {
            try
            {
                var resp = await _productService.createProduct(data);
                if (resp == true)
                {
                    return Ok(new OkDTO
                    {
                        message = "Producto creado correctamente",
                        ok = true,
                    });
                } else{
                    throw new Exception("Error al crear producto");
                }
            } catch (Exception ex)
            {
                return BadRequest(new OkDTO
                {
                    message = ex.Message,
                    ok = true,
                });
            }
        }
    }
}
