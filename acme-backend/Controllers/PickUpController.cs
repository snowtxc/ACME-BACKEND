using acme_backend.Models;
using acme_backend.Models.Dtos.Empresa;
using acme_backend.Models.Dtos;
using acme_backend.Models.Dtos.Pickup;
using acme_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PickUpController : Controller
    {
        private readonly PickupService _pickupService;
        private readonly AuthService _authService;

        public PickUpController(PickupService pickupService, AuthService authService = null)
        {
            _pickupService = pickupService;
            _authService = authService;
        }



        [HttpGet]
        public async Task<IActionResult> list()
        {
            try
            {
                string userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<PickupDto> pickups= await _pickupService.list(userLoggedId);
                return Ok(pickups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> create(PickupCreateDto pickupCreate)
        {
           
            try
            {
                string userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                PickupDto pickupCreated = await _pickupService.create(userLoggedId, pickupCreate);
          
                return Ok(pickupCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpPost]
        [Route("deletesById")]
        public async Task<IActionResult> deletesByIds(PickupsForDelete pickupsForDeletes)
        {
            try
            {
                List<PickupDto> pickupsDeleted = await _pickupService.deletesByIds(pickupsForDeletes.pickupsIds);
                return Ok(pickupsDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }





    }
}
