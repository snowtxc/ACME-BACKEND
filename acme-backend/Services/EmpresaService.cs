using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;

namespace acme_backend.Services
{
    public class EmpresaService
    {
        private ApplicationDbContext _db;

        public EmpresaService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<EmpresaDto> create(EmpresaDto newCompanyDto)
        {
         
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
                await _db.SaveChangesAsync();
                newCompanyDto.Id = newCompany.Id;

                return newCompanyDto;
        }




    }
}
