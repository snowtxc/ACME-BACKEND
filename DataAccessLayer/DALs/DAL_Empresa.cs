using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Empresa;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.IDALs
{
    public class DAL_Empresa : IDAL_Empresa
    {
        private ApplicationDbContext _db;
        private UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DAL_Empresa(ApplicationDbContext db, UserManager<Usuario> userManager, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<EmpresaDto> create(EmpresaCreateDto newCompanyDto)
        {
            if (await _userManager.FindByEmailAsync(newCompanyDto.EmailUsuarioAdmin) != null)
            {
                throw new Exception("No puedes asignarle este email al usuario ya que ya existe");
            }


            Empresa newCompany = new Empresa
            {
                Nombre = newCompanyDto.Nombre,
                Correo = newCompanyDto.Correo,
                Direccion = newCompanyDto.Direccion,
                CostoEnvio = newCompanyDto.CostoEnvio,
                Imagen = newCompanyDto.Imagen,
                Telefono = newCompanyDto.Telefono,
                Wallet = newCompanyDto.Wallet
            };
            _db.Empresas.Add(newCompany);
            _db.SaveChanges();
            LookAndFeel lookNFeel = new LookAndFeel
            {
                ColorPrincipal = newCompanyDto.ColorPrincipal,
                ColorSecundario = newCompanyDto.ColorSecundario,
                ColorFondo = newCompanyDto.ColorFondo,
                Empresa = newCompany,
                EmpresaId = newCompany.Id,
                LogoUrl = newCompanyDto.Imagen,
                NombreSitio = newCompanyDto.Nombre,
            };
            _db.LooksAndFeels.Add(lookNFeel);
            _db.SaveChanges();
            Usuario user = new Usuario
            {
                Email = newCompanyDto.EmailUsuarioAdmin,
                Nombre = newCompanyDto.NombreUsuarioAdmin,
                Celular = newCompanyDto.TelefonoUsuarioAdmin,
                EmpresaId = newCompany.Id,
                UserName = newCompanyDto.EmailUsuarioAdmin,
                Empresa = newCompany,

            };


            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Vendedor");
                DAL_Mail mailService = new DAL_Mail();

                string path = @"./Templates/ActivateAccount.html";
                string activateToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string content = File.ReadAllText(path);
                string withUserName = content.Replace("{{ userName }}", user.Nombre);
                var activateAccountPath = _configuration["FrontendURL"] + "/reset-password?token=" + activateToken + "&email=" + user.Email;
                string newContent = withUserName.Replace("{{ activateAccountLink }}", activateAccountPath);

                mailService.sendMail(user.Email, "Activa tu cuenta", newContent);
            }

            await _db.SaveChangesAsync();

            EmpresaDto empresa = _mapper.Map<EmpresaDto>(newCompany);
            return empresa;
        }

        public async Task<List<EmpresaDto>> deletesByIds(int[] empresasIds)
        {
            List<Empresa> empresasToDeletes = await  _db.Empresas.Where(e => empresasIds.Contains(e.Id)).ToListAsync();
            List<EmpresaDto> eliminatedEmpresas = new List<EmpresaDto>();
            foreach (Empresa empresaDeleted in empresasToDeletes)
            {
                if (empresaDeleted != null)
                {
                    empresaDeleted.Activo = false;
                    _db.Empresas.Update(empresaDeleted);
                    await _db.SaveChangesAsync();
                    eliminatedEmpresas.Add(new EmpresaDto { Id = empresaDeleted.Id, Correo = empresaDeleted.Correo, CostoEnvio = empresaDeleted.CostoEnvio, Direccion = empresaDeleted.Direccion, Telefono = empresaDeleted.Telefono, Nombre = empresaDeleted.Nombre, Imagen = empresaDeleted.Imagen, Wallet = empresaDeleted.Wallet });
                } 
           
            }
            return eliminatedEmpresas;
        }


        public async Task<EmpresaDto> getById(int id)
        {
            Empresa? empresa = await _db.Empresas
                .Include(e => e.LookAndFeel)
                .Include((e) => e.LookAndFeel.CategoriaDestacada)
                .FirstOrDefaultAsync(e => e.Id == id);
            //.FindAsync(id);
            if (empresa == null)
            {
                throw new Exception("Empresa no existe");
            }
            EmpresaDto empresaDto = new EmpresaDto {
                Id = empresa.Id,
                Correo = empresa.Correo,
                CostoEnvio = empresa.CostoEnvio,
                Direccion = empresa.Direccion,
                Telefono = empresa.Telefono,
                Nombre = empresa.Nombre,
                Imagen = empresa.Imagen,
                Wallet = empresa.Wallet,
                LookAndFeel = _mapper.Map<LookAndFeelDTO>(empresa.LookAndFeel)
            };
            return empresaDto;

        }

        public async Task<List<EmpresaDto>> List()
        {
            List<EmpresaDto> empresas = await _db.Empresas
                .Where(e => e.Activo == true)
                .Include((empresa) => empresa.LookAndFeel)
                .Include((empresa) => empresa.LookAndFeel.CategoriaDestacada)
                .Select(e => new EmpresaDto
                {
                    Id = e.Id,
                    Correo = e.Correo,
                    CostoEnvio = e.CostoEnvio,
                    Direccion = e.Direccion,
                    Telefono = e.Telefono,
                    Nombre = e.Nombre,
                    Imagen = e.Imagen,
                    Wallet = e.Wallet,
                    LookAndFeel = _mapper.Map<LookAndFeelDTO>(e.LookAndFeel)
                })
                .ToListAsync();
            return empresas;
        }
    }
}
