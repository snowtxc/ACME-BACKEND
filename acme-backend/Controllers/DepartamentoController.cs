﻿using acme_backend.Models.Dtos.Ciudad;
using acme_backend.Models.Dtos.Departamento;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoService _departamentoService;
        private readonly CiudadService _ciudadService;

        public DepartamentoController(DepartamentoService deptoService, CiudadService ciudadService)
        {
            _departamentoService = deptoService;
            _ciudadService = ciudadService;
        }

        [HttpGet]
        public async Task<IActionResult> listarDepartamentos()
        {
            try
            {
                var departamentos = await _departamentoService.listarDepartamentos();
                return Ok(departamentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getDepartamentoById(int id)
        {
            try
            {
                var departamento = await _departamentoService.getDepartamentoById(id);
                return Ok(departamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> createDepartamento(DepartamentoCreateDTO depto)
        {
            try
            {
                var createdDepto = await _departamentoService.createDepartamento(depto);
                return CreatedAtAction(nameof(getDepartamentoById), new { id = createdDepto.Id }, createdDepto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDepartamento(DepartamentoEditDTO departamento)
        {
            try
            {
                var updatedDepartamento = await _departamentoService.updateDepartamento(departamento);
                return Ok(updatedDepartamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDepartamento(int id)
        {
            try
            {
                await _departamentoService.deleteDepartamento(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/ciudades")]
        public async Task<IActionResult> getCiudadesByDepartamento(int id)
        {
            try
            {
                List<CiudadDTO> ciudades  = await _ciudadService.listarCiudadesByDepartamento(id);
                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}