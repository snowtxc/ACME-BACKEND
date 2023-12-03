using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IBL_Producto _productService;

        public ProductoController(IBL_Producto prodService)
        {
            _productService = prodService;
        }


        [HttpGet, Route("mis-productos")]
        [Authorize(Roles = "Vendedor,Usuario")]
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
                    throw new Exception("Error al cargar producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("buscar-productos")]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> buscarProductos([FromQuery] string query, [FromQuery] int empresaId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null) {
                    var productos = await _productService.listarProductos(empresaId, query);
                    return Ok(productos);
                }
                else
                {
                    throw new Exception("Error al cargar producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("productos-empresa")]
        [Authorize(Roles = "Vendedor,Usuario")]
        public async Task<IActionResult> listarProductosByEmpresa(int empresaId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var productos = await _productService.listarProductos(empresaId, "");
                    return Ok(productos);
                }
                else
                {
                    throw new Exception("Error al cargar producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("{productoId}")]
        [Authorize(Roles = "Vendedor,Usuario")]
        public async Task<IActionResult> obtenerProductoById(int productoId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var producto = await _productService.obtenerProductoById(userId.Value, productoId);
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


        [HttpPost, Route("relacionados")]
        [Authorize(Roles = "Vendedor,Usuario")]
        public async Task<IActionResult> obtenerProductosRelacionados(int[] productosIds)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var productos = await _productService.obtenerProductosRelacionados(userId.Value, productosIds);
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

        [HttpDelete, Route("{productoId}")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> eliminarProducto(int productoId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var productoResp = await _productService.deshabilitarProducto(userId.Value, productoId);
                    return Ok(new
                    {
                        ok = productoResp,
                        message = "Producto eliminado correctamente"
                    });
                }
                else
                {
                    throw new Exception("Error al crear producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost, Route("updateProduct")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> editarProducto(EditProductoDTO productoInfo)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var productoResp = await _productService.editProducto(productoInfo, userId.Value);
                    return Ok(new
                    {
                        ok = productoResp,
                        message = "Producto actualizado correctamente"
                    });
                }
                else
                {
                    throw new Exception("Error al actualizar producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Vendedor")]
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
                }
                else
                {
                    throw new Exception("Usuario invalido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new OkDTO
                {
                    message = ex.Message,
                    ok = true,
                });
            }
        }

        [HttpPost, Route("calificarProducto")]
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> calificarProducto(CreateCalificacionDTO calificacionDto)
        {
            try
            {

                string userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userLoggedId == null)
                {
                    throw new Exception("Usuario logueado inválido.");
                }

                await _productService.calificarProducto(userLoggedId, calificacionDto);
                return Ok(new
                {
                    ok = true,
                    message = "Calificación guardada correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    message = ex.Message
                });
            }
        }
    }
}
