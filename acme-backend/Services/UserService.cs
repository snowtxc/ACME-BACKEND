using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Shared.Utils;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserService(UserManager<Usuario> userManager, IConfiguration configuration, ApplicationDbContext db, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _db = db;
            _mapper = mapper;
        }

        public async Task<UsuarioListDto> getUserById(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Direcciones)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var direccionMapper = new DireccionMapper(_mapper);
            var userDto = new UsuarioListDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Celular = user.Celular,
                Email = user.Email,
                Imagen = user.Imagen,
                EmpresaId = (int)user.EmpresaId,
                Direcciones = user.Direcciones.Select(direccion => direccionMapper.MapDireccionToDto(direccion)).ToList(),
                Calificaciones = _db.Calificaciones.Count(c => c.UsuarioId == user.Id),
            };
            return userDto;
        }

        public async Task<List<UsuarioListDto>> listUsers(string userId)
        {
            var userInfo = await _userManager.Users
          .Include(u => u.Empresa)
          .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            var empresaId = userInfo.Empresa?.Id;


            var direccionMapper = new DireccionMapper(_mapper);
            var users = await _userManager.Users
                .Include(u => u.Empresa)
                .Where(u => u.Empresa.Id == empresaId)
                .Select(u => new UsuarioListDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Celular = u.Celular,
                    Imagen = u.Imagen,
                    EmpresaId = u.Empresa.Id,
                    Direcciones = u.Direcciones.Select(direccion => direccionMapper.MapDireccionToDto(direccion)).ToList(),
                    Calificaciones = _db.Calificaciones.Count(c => c.UsuarioId == u.Id),
                })
                .ToListAsync();
            return users;
        }

        public async Task<UsuarioCreateDto> createUser(string userId, UsuarioCreateDto userDto)
        {
            var userInfo = await _userManager.Users
           .Include(u => u.Empresa)
           .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            var empresaId = userInfo.Empresa?.Id;


            var user = new Usuario
            {
                Email = userDto.Email,
                Nombre = userDto.Nombre,
                Celular = userDto.Celular,
                Imagen = userDto.Imagen,
                EmpresaId = userInfo.Empresa.Id,
                UserName = userDto.Email,
                Empresa = userInfo.Empresa,

            };

            var foundCiudad = await _db.Ciudades.FindAsync(userDto.Direccion.CiudadId);
            Direccion dir = new Direccion
            { 
                Calle = userDto.Direccion.Calle,
                CalleEntre1 = userDto.Direccion.CalleEntre1,
                CalleEntre2 = userDto.Direccion.CalleEntre2,
                NroPuerta = userDto.Direccion.NroPuerta,
                Ciudad = foundCiudad,
                CiudadId = userDto.Direccion.CiudadId,
            };
            user.Direcciones.Add(dir);

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Vendedor");
                userDto.Id = user.Id;
                userDto.Direccion.Id = user.Direcciones.First().Id;
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

