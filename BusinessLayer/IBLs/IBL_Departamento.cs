using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Departamento
    {

        public Task<DepartamentoDTO> createDepartamento(DepartamentoCreateDTO depto);
        public Task<List<DepartamentoDTO>> listarDepartamentos();
        public Task<DepartamentoDTO> getDepartamentoById(int id);
        public Task<DepartamentoDTO> updateDepartamento(DepartamentoEditDTO departamento);
        public Task deleteDepartamento(int id);
    }
}
