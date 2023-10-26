using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Empresa;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IBLs;

namespace BusinessLayer.BLs
{
    public class BL_Empresa: IBL_Empresa
    {
        private IDAL_Empresa _empres;

        public BL_Empresa(IDAL_Empresa empresa)
        {
            _empres = empresa;
        }

        public Task<EmpresaDto> create(EmpresaCreateDto newCompanyDto)
        {
            return _empres.create(newCompanyDto);
        }
        public Task<List<EmpresaDto>> deletesByIds(int[] empresasIds)
        {
            return _empres.deletesByIds(empresasIds);
        }
        public Task<EmpresaDto> getById(int id)
        {
            return _empres.getById(id);
        }
        public Task<List<EmpresaDto>> List()
        {
            return _empres.List();
        }
    }
}
