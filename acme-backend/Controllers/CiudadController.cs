﻿using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Ciudad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadController : ControllerBase
    {
        private readonly IBL_Ciudad _ciudadService;

        public CiudadController(IBL_Ciudad ciudadService)
        {
            _ciudadService = ciudadService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> listarCiudades()
        {
            try
            {
                var ciudades = await _ciudadService.listarCiudades();
                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Vendedor,Usuario")]
        public async Task<IActionResult> getCiudadById(int id)
        {
            try
            {
                var ciudad = await _ciudadService.getCiudadById(id);
                return Ok(ciudad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> createCiudad(CiudadCreateDTO ciudad)
        {
            try
            {
                var createdCiudad = await _ciudadService.createCiudad(ciudad);
                return CreatedAtAction(nameof(getCiudadById), new { id = createdCiudad.Id }, createdCiudad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateCiudad(CiudadDTO ciudad)
        {
            try
            {
                var updatedCiudad = await _ciudadService.updateCiudad(ciudad);
                return Ok(updatedCiudad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteCiudad(int id)
        {
            try
            {
                await _ciudadService.deleteCiudad(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
