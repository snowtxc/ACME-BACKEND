using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace acme_backend.Services
{
    public class AuthService
    {
        private ApplicationDbContext _db;
        private readonly IServiceProvider _serviceProvider;

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _db = db;
        }

        public async Task<string> generateTokenByUser(Usuario user)
        {
            try
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // Puedes agregar otros claims según tus necesidades
                    new Claim(ClaimTypes.Name, user.Nombre),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenStr;
                throw new Exception("Credenciales invalidas");
            } catch (Exception ex)
            {
                throw new Exception("Invalid token");
            }
        }

        public async Task<string> login(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                if (user.EmailConfirmed == false)
                {
                    throw new Exception("Activa la cuenta para continuar");
                }
                var token = await this.generateTokenByUser(user);
                return token;
            }
            throw new Exception("Credenciales invalidas");
        }

        public async Task<bool> resetPassword(ResetPasswordInputDTO data)
        {
            try
            {
                string token = data.Token;
                string newPassword = data.Password;
                var user = await _userManager.FindByEmailAsync(data.Email);
                if (user == null)
                {
                    throw new Exception("Usuario invalido");
                }
                user.EmailConfirmed = true;
                var result = await _userManager.ResetPasswordAsync(user, data.Token, data.Password);
                return result.Succeeded;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // this method will only create users with role "Usuario"
        public async Task<UsuarioDto?> register(UsuarioDto userDto)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(userDto.Email) != null)
                {
                    throw new Exception("Email ya esta en uso");
                }
                Usuario user = new Usuario
                {
                    Nombre = userDto.Nombre,
                    Celular = userDto.Celular,
                    Imagen = userDto.Imagen,
                    UserName = userDto.Email,
                    Email = userDto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var result = await _userManager.CreateAsync(user, "DefaultPass1/");
                userDto.Id = user.Id;
                MailService mailService = new MailService();

                string path = @"./Templates/ActivateAccount.html";
                string activateToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string content = File.ReadAllText(path);
                string withUserName = content.Replace("{{ userName }}", user.Nombre);
                var activateAccountPath = _configuration["FrontendURL"] + "/reset-password?token=" + activateToken + "&email=" + user.Email;
                string newContent = withUserName.Replace("{{ activateAccountLink }}", activateAccountPath);


                mailService.sendMail(user.Email, "Activa tu cuenta", newContent);
                return userDto;
            } catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> forgotPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new Exception("Usuario invalido");
                }
                MailService mailService = new MailService();

                string path = @"./Templates/ResetPassword.html";
                string activateToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string content = File.ReadAllText(path);
                string withUserName = content.Replace("{{ userName }}", user.Nombre);
                var activateAccountPath = _configuration["FrontendURL"] + "/reset-password?token=" + activateToken + "&email=" + user.Email;
                string newContent = withUserName.Replace("{{ activateAccountLink }}", activateAccountPath);

                mailService.sendMail(user.Email, "Restablece tu contraseña", newContent);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task addRoleToUser(string id, string role)
        {
            try
            {
                //DisableForeignKeys();
                var currentUser = await _userManager.FindByIdAsync(id);
                if (currentUser == null)
                {
                    throw new Exception("El usuario no exsite");
                }
                await _userManager.AddToRoleAsync(currentUser, role);
                //EnableForeignKeys();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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

        public async Task<UserInfoDTO> getUserInfoById(string userId)
        {
            try
            {
                Console.WriteLine("userId " + userId);
              var user = await _userManager.FindByIdAsync(userId);
              if (user == null)
                {
                    throw new Exception("Error al obtener usuario");
                }
                var roles = await _userManager.GetRolesAsync(user);

                Console.WriteLine(roles);
              UserInfoDTO uinfo = new UserInfoDTO();
                uinfo.Celular = user.Celular;
                uinfo.Email = user.Email;
                uinfo.Imagen = user.Imagen;
                uinfo.Roles = roles;
                uinfo.Id = user.Id;
                uinfo.Nombre = user.Nombre;
                if (user.Empresa != null)
                {
                    uinfo.EmpresaId = user.Empresa.Id;
                }
                return uinfo;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> createUserWithExternalService(LoginWithCredentialsDTO userInfo)
        {
            try
            {
                if (_configuration["LoginExternalSerciveSecret"] != userInfo.SecretWord)
                {
                    throw new Exception("Invalid secret");
                }
                var existsUser = await _userManager.FindByIdAsync(userInfo.Uid);
                if (existsUser != null)
                {
                    var token = await this.generateTokenByUser(existsUser);
                    return token;
                }
                else
                {
                    var userExists = await _userManager.FindByEmailAsync(userInfo.Email);
                    if (userExists != null)
                    {
                        throw new Exception("Email ya esta en uso");
                    }
                    Usuario user = new Usuario
                    {
                        Id = userInfo.Uid,
                        Nombre = userInfo.Name,
                        Imagen = userInfo.Imagen != null ? userInfo.Imagen : "https://cdn.pixabay.com/photo/2012/04/26/19/43/profile-42914_1280.png",
                        UserName = userInfo.Email,
                        Email = userInfo.Email,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    var result = await _userManager.CreateAsync(user);
                    await this.addRoleToUser(user.Id, "Usuario");
                    var token = await this.generateTokenByUser(user);
                    return token;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }





}
   
