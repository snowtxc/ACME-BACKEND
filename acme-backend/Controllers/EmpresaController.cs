using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Models.Dtos.Empresa;
using acme_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;


        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService =  empresaService;

        }

        [HttpPost]
        [Route("create")]
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


    }
}
