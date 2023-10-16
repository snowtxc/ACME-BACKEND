using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

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
                if (registrationResult != null)
                {
                   await _authService.addRoleToUser(registrationResult.Id, "Usuario");
                }
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

        [HttpGet]
        [Route("currentUser")]
        public async Task<IActionResult> loggedUserInfo()
        {
            try
            {
                if (User != null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userInfo = await _authService.getUserInfoById(userId);
                    return Ok(new { User = userInfo });

                } else
                {
                    return BadRequest("Usuario invalido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> forgotPassword(ForgotPasswordInputDTO data)
        {
            try
            {
                var result = await _authService.forgotPassword(data.Email);
                return Ok(new { Ok = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> resetPassword(ResetPasswordInputDTO data)
        {
            try
            {
                var result = await _authService.resetPassword(data);
                return Ok(new { Ok = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("loginWithExternalService")]
        public async Task<IActionResult> loginWithExternalService(LoginWithCredentialsDTO loginDto)
        {
            try
            {
                string token = await _authService.createUserWithExternalService(loginDto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
