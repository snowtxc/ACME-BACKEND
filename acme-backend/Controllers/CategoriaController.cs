
using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccessLayer.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly IBL_Categoria _categoriaService;

        public CategoriaController(IBL_Categoria catService)
        {
            _categoriaService = catService;
        }


        [HttpGet]
        [Authorize(Roles = "Vendedor")]
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
        [HttpGet, Route("categorias-de-empresa")]
        [Authorize(Roles = "Vendedor,Usuario")]
        public async Task<IActionResult> listarCategoriasByEmpresa(int empresaId)
        {
            try
            {
                var default_Categories = new string[] { };
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    var categorias = await _categoriaService.listarCategoriasByEmpresa(empresaId);
                    return Ok(categorias);
                }
                else
                {
                    return Ok(default_Categories);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> crearCategoria(CreateCategoriaDTO categoria)
        {
            try
            {
                var default_Categories = new string[] { };
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    await _categoriaService.crearCategoria(categoria, userId.Value);
                    return Ok(new OkDTO
                    {
                        ok = true,
                        message = "Categoria creada correctamente"
                    });
                }
                else
                {
                    return Ok(new OkDTO
                    {
                        ok = false,
                        message = "error al crear categoria"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new OkDTO
                {
                    ok = true,
                    message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> borrarCategorias(int[] categoriasIds)
        {
            try
            {
                var default_Categories = new string[] { };
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    await _categoriaService.borrarCategorias(categoriasIds, userId.Value);
                    return Ok(new OkDTO
                    {
                        ok = true,
                        message = "Categorias eliminadas correctamente"
                    });
                }
                else
                {
                    return Ok(new OkDTO
                    {
                        ok = false,
                        message = "error al eliminar categorias"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new OkDTO
                {
                    ok = true,
                    message = ex.Message
                });
            }
        }
    }
}
