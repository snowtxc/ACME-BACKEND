
using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritoController : ControllerBase
    {
        private readonly IBL_Carrito _carritoService;

        public CarritoController(IBL_Carrito carrService)
        {
            _carritoService = carrService;
        }


        [HttpPost]
        public async Task<IActionResult> crear(AgregarProductoCarritoDTO data)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var added = await _carritoService.agregarProductoCarrito(data, userId.Value);
                    if (added == true)
                    {
                        return Ok(new OkDTO
                        {
                            message = "Producto agregado correctamente",
                            ok = true,
                        });
                    }
                    else
                    {
                        throw new Exception("Error al agregar producto al carrito");
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
                    ok = false,
                    message = ex.Message,
                });
            }
        }

        [HttpGet, Route("obtenerCarrito")]
        public async Task<IActionResult> obtenerCarrito(int EmpresaId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var productos = await _carritoService.obtenerCarrito(EmpresaId, userId.Value);
                    return Ok(productos);
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
                    ok = false,
                    message = ex.Message,
                });
            }
        }

        [HttpDelete, Route("borrarLinea")]
        public async Task<IActionResult> eliminarLinea(int LineaId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var deleted = await _carritoService.borrarCarritoLinea(LineaId, userId.Value);
                    if (deleted == true)
                    {
                        return Ok(new OkDTO
                        {
                            ok = true,
                            message = "Linea eliminada correctamente"
                        });
                    } else
                    {
                        throw new Exception("Error al borrar linea");
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
                    ok = false,
                    message = ex.Message,
                });
            }
        }
    }
}
