

using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Estadisticas
    {
        Task<SortEstadisticasDTO> listarEstadisticas();
        Task<AdminEstadisticasDTO> listarEstadisticasAdmin();
        Task<EmpresaEstadisticasDTO> listarEstadisticasEmpresa(int empresaId);
    }
}
