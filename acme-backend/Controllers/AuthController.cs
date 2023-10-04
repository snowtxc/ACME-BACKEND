using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;


        public AuthController(AuthService authService)
        {
            _authService = authService;

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(UsuarioDto userDto)
        {
            try
            {
                var registrationResult = await _authService.register(userDto);
                return Ok(new { User = registrationResult });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginDto loginDto)
        {
            try
            {
                string token = await _authService.login(loginDto.Email, loginDto.Password);
                return Ok(new { Token = token});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
