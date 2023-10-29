using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Envio;
using Microsoft.AspNetCore.Mvc;

namespace SHIPPING.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : Controller
    {

        [HttpPost("createPackage")]

        public IActionResult createPackage(EnvioRequestDto envioRequestDto)
        {
            IBL_Shipping bL_Shipping = new BL_Shipping();
            EnvioRastreoResponseDto response  =  bL_Shipping.createPackage(envioRequestDto);
            return Ok(response);
            
        }
    }
}
