
using BusinessLayer.IBLs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Empresa;
using DataAccessLayer.Models.Dtos.Pickup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IBL_Empresa _empresaService;
        private readonly IBL_Compra _compraIbl;

        public EmpresaController(IBL_Empresa empresaService, IBL_Compra compraIbl)
        {
            _empresaService = empresaService;
            _compraIbl = compraIbl;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> register(EmpresaCreateDto empresaDto)
        {
            try
            {
                EmpresaDto companyCreated = await _empresaService.create(empresaDto);
                return Ok(companyCreated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Usuario,Vendedor")]
        public async Task<IActionResult> listar()
        {
            try
            {
                List<EmpresaDto> empresas = await _empresaService.List();
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                EmpresaDto empresa = await _empresaService.getById(id);
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }



        [HttpPost]
        [Route("deletesById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deletesByIds(EmpresaForDeletes empresaForDeletes)
        {
            try
            {
                List<EmpresaDto> empresasDeleted = await _empresaService.deletesByIds(empresaForDeletes.empresasIds);
                return Ok(empresasDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost]
        [Route("editLookAndFeel")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> editLookAndFeel(LookAndFeelDTO laf)
        {
            try
            {
                string userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                LookAndFeelDTO lookAndFeel = await _empresaService.editLookAndFeel(userLoggedId, laf);
                return Ok(lookAndFeel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("ventas")]
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<IActionResult> getVentasByEmpresa()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                EmpresaDto empresa =  await _empresaService.getByUser(userId);
                List<SortCompra> compras  = await this._compraIbl.listByEmpresa(empresa.Id);
                return Ok(compras);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
