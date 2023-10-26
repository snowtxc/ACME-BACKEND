using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Pickup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace acme_backend.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PickUpController : Controller
    {
        private readonly IBL_Pickup _pickupService;
        private readonly IBL_Auth _authService;

        public PickUpController(IBL_Pickup pickupService, IBL_Auth authService = null)
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
