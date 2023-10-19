using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet, Route("mis-productos"), Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> listarProductos()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var productos = await _productService.listarProductosDeMiEmpresa(userId);
                    return Ok(productos);
                }
                else
                {
                    throw new Exception("Error al crear producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("{productoId}"), Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> obtenerProductoById(int productoId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var producto = await _productService.obtenerProductoById(userId ,productoId);
                    return Ok(producto);
                }
                else
                {
                    throw new Exception("Error al crear producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> crearProducto(CrearProductoDTO data)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var resp = await _productService.createProduct(data, userId);
                    if (resp == true)
                    {
                        return Ok(new OkDTO
                        {
                            message = "Producto creado correctamente",
                            ok = true,
                        });
                    }
                    else
                    {
                        throw new Exception("Error al crear producto");
                    }
                } else
                {
                    throw new Exception("Usuario invalido");
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
