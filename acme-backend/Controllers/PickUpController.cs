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
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> list(PickupCreateDto pickupCreate)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Vendedor")]
        public async Task<IActionResult> delete(int pickupId)
        {
            try
            {
                string userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                PickupDto pickupDeleted = await _pickupService.delete(userLoggedId, pickupId);
                return Ok(pickupDeleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
