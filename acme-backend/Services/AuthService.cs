using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace acme_backend.Services
{
    public class AuthService
    {
        private ApplicationDbContext _db;

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public AuthService(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }


        public async Task<string> login(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nombre),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenStr;
            }
            throw new Exception("Credenciales invalidas");
        }

        public async Task<UsuarioDto?> register(UsuarioDto userDto)
        {
           
            if(await _userManager.FindByEmailAsync(userDto.Email) != null)
            {
                throw new Exception("Email ya esta en uso");
            }
            Usuario user = new Usuario
            {
                Nombre =  userDto.Nombre,
                Celular = userDto.Celular,
                Imagen = userDto.Imagen,
                UserName = userDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    throw new Exception(item.Description);

                }
            }
            userDto.Id = user.Id;
            return userDto;

        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


        public async Task<UsuarioDto?> FindByEmail(string email)
        {

            Usuario ?  user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {   
                UsuarioDto userDto =  new UsuarioDto { Nombre = user.Nombre, Email = user.Email , Celular = user.Celular, Imagen = user.Imagen };
                return userDto;
            }
            return null;

        }
    }

   



}
   
