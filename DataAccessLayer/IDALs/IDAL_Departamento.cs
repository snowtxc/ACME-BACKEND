

using DataAccessLayer.Models.Dtos.Departamento;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Departamento
    {
        Task<DepartamentoDTO> createDepartamento(DepartamentoCreateDTO depto);
        Task<List<DepartamentoDTO>> listarDepartamentos();
        Task<DepartamentoDTO> getDepartamentoById(int id);
        Task<DepartamentoDTO> updateDepartamento(DepartamentoEditDTO departamento);
        Task deleteDepartamento(int id);
    }
}
