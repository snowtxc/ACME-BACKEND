using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBL_Users _userService;

        public UserController(IBL_Users userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Vendedor")]
        public async Task<ActionResult<List<UsuarioListDto>>> listUsers()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId == null)
                {
                    return BadRequest("Usuario logeado invalido");

                }
                var users = await _userService.listUsers(userId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioListDto>> getUserById(string id)
        {
            try
            {
                var user = await _userService.getUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> createUser(UsuarioCreateDto userDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId == null)
                {
                    return BadRequest("Usuario logeado invalido");

                }
                var newUser = await _userService.createUser(userId,userDto);

                if (newUser == null)
                {
                    Console.WriteLine("Error on create user");
                    return BadRequest();
                }

                return CreatedAtAction(nameof(getUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> updateUser(UpdateUsuarioDto userDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId == null)
                {
                    return BadRequest("Usuario logeado inválido.");

                }
                await _userService.updateUser(userId, userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(string id)
        {
            try
            {
                await _userService.deleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarDirecciones")]
        public async Task<ActionResult<List<DireccionDTO>>> getLoggUsrDirecciones()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId == null)
                {
                    return BadRequest("Usuario logeado inválido.");

                }
                var direcciones = await _userService.listLoggUsrDirecciones(userId);
                return Ok(direcciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("agregarDireccion")]
        public async Task<IActionResult> agregarDireccion(DireccionDTO direccion)
        {
            try
            {
                if (User != null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _userService.agregarDireccion(userId, direccion);
                    return Ok();
                }
                else
                {
                    return BadRequest("Usuario inválido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("modificarDireccion")]
        public async Task<IActionResult> modificarDireccion(DireccionDTO direccion)
        {
            try
            {
                if (User != null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _userService.modificarDireccion(userId, direccion);
                    return Ok();
                }
                else
                {
                    return BadRequest("Usuario inválido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
