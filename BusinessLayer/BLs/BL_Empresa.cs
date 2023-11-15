using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Empresa;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Pickup;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BusinessLayer.BLs
{
    public class BL_Empresa: IBL_Empresa
    {
        private IDAL_Empresa _empres;
        private readonly UserManager<Usuario> _userManager;
        private  IMapper _mapper;


        public BL_Empresa(IDAL_Empresa empresa, UserManager<Usuario> userManager,IMapper mapper)
        {
            _empres = empresa;
            _userManager = userManager;
            _mapper = mapper;
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
        public Task<LookAndFeelDTO> editLookAndFeel(string userLoggedId, LookAndFeelDTO laf)
        {
            return _empres.editLookAndFeel(userLoggedId, laf);
        }

      
        public async Task<EmpresaDto> getByUser(string userId)
        {
            var user = await _userManager.Users.Include(u => u.Empresa).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("Usuario no existe");
            }
            EmpresaDto empresa = _mapper.Map<EmpresaDto>(user.Empresa);
            return empresa;
        }
    }
}
