using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Empresa;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Dtos.Pickup;

namespace BusinessLayer.IBLs
{
    public interface IBL_Empresa
    {

        public Task<EmpresaDto> create(EmpresaCreateDto newCompanyDto);
        public Task<List<EmpresaDto>> deletesByIds(int[] empresasIds);
        public Task<EmpresaDto> getById(int id);
        public Task<List<EmpresaDto>> List();
        public Task<LookAndFeelDTO> editLookAndFeel(string userLoggedId, LookAndFeelDTO laf);
    }
}
