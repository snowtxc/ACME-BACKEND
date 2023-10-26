using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Departamento: IBL_Departamento
    {
        private IDAL_Departamento _dep;

        public BL_Departamento(IDAL_Departamento dep)
        {
            _dep = dep;
        }

        public Task<DepartamentoDTO> createDepartamento(DepartamentoCreateDTO depto)
        {
            return _dep.createDepartamento(depto);
        }
        public Task<List<DepartamentoDTO>> listarDepartamentos()
        {
            return _dep.listarDepartamentos();
        }
        public Task<DepartamentoDTO> getDepartamentoById(int id)
        {
            return _dep.getDepartamentoById(id);
        }
        public Task<DepartamentoDTO> updateDepartamento(DepartamentoEditDTO departamento)
        {
            return _dep.updateDepartamento(departamento);
        }
        public Task deleteDepartamento(int id)
        {
            return _dep.deleteDepartamento(id);
        }
    }
}
