using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Http;
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
        [Route("register")]
        public async Task<IActionResult> register(EmpresaDto empresaDto)
        {
            try
            {
                EmpresaDto companyCreated = await _empresaService.create(empresaDto);
                return Ok(companyCreated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ha ocurrido un error inesperado");
            }


        }



    }
}
