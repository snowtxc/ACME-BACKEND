using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;


namespace acme_backend.Services
{
    public class UserService
    {
        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<Usuario> userManager, IConfiguration configuration, ApplicationDbContext db)
        {
            _configuration = configuration;
            _userManager = userManager;
            _db = db;
        }

        public async Task<UsuarioListDto> getUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var userDto = new UsuarioListDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Celular = user.Celular,
                Email = user.Email,
                Imagen = user.Imagen,
                EmpresaId = (int)user.EmpresaId,
            };
            return userDto;
        }

        public async Task<List<UsuarioListDto>> listUsers()
        {
            var users = await _userManager.Users
                .Select(u => new UsuarioListDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Celular = u.Celular,
                    Imagen = u.Imagen,
                    EmpresaId = (int)u.EmpresaId,
                })
                .ToListAsync();
            return users;
        }

        public async Task<UsuarioCreateDto> createUser(UsuarioCreateDto userDto)
        {
            var empresa = await _db.Empresas.FindAsync(userDto.EmpresaId);
            var user = new Usuario
            {
                Email = userDto.Email,
                Nombre = userDto.Nombre,
                Celular = userDto.Celular,
                Imagen = userDto.Imagen,
                EmpresaId = userDto.EmpresaId,
                UserName = userDto.Email,
                Empresa = empresa,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Usuario");
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
            }
            if (result.Errors.Any())
            {
                throw new Exception(result.Errors.First().Description);
            }
            return null;
        }

        public async Task updateUser(string id, UsuarioDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Nombre = userDto.Nombre;
                user.Email = userDto.Email;
                user.Celular = userDto.Celular;
                user.Imagen = userDto.Imagen;

                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Usuario no encontrado.");
            }
        }

        public async Task deleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            else
            {
                throw new Exception("Usuario no encontradpo.");
            }
        }
    }
}

