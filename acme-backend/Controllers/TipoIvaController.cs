using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.TipoIVA;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoIvaController : ControllerBase
    {
        private readonly IBL_TipoIVA _tipoIvaService;

        public TipoIvaController(IBL_TipoIVA tipoIvaService)
        {
            _tipoIvaService = tipoIvaService;
        }


        [HttpGet]
        public async Task<IActionResult> listarTiposIVA()
        {
            try
            {
                var categorias = await _tipoIvaService.listarTiposIVA();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getTipoIvaById(int id)
        {
            try
            {
                var tipoIva = await _tipoIvaService.getTipoIvaById(id);
                return Ok(tipoIva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> createTipoIva(TipoIVADTO tipoIva)
        {
            try
            {
                var createdTipoIva = await _tipoIvaService.createTipoIva(tipoIva);
                return CreatedAtAction(nameof(getTipoIvaById), new { id = createdTipoIva.Id }, createdTipoIva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateTipoIva(TipoIVADTO tipoIva)
        {
            try
            {
                var updatedTipoIva = await _tipoIvaService.updateTipoIva(tipoIva);
                return Ok(updatedTipoIva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTipoIva(int id)
        {
            try
            {
                await _tipoIvaService.deleteTipoIva(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
