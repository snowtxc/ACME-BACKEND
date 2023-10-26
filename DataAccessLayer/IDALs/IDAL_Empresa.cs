

using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Empresa;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Empresa
    {
        Task<EmpresaDto> create(EmpresaCreateDto newCompanyDto);
        Task<List<EmpresaDto>> deletesByIds(int[] empresasIds);
        Task<EmpresaDto> getById(int id);
        Task<List<EmpresaDto>> List();
    }
}
