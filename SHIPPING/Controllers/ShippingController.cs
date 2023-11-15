using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Envio;
using Microsoft.AspNetCore.Mvc;
using SHIPPING.Dtos;

namespace SHIPPING.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : Controller
    {
        private IBL_Compra _compraService;

        public ShippingController(IBL_Compra compraSrv) {
            _compraService = compraSrv;
        }

        [HttpPost("createPackage")]

        public IActionResult createPackage(EnvioRequestDto envioRequestDto)
        {

            IBL_Shipping bL_Shipping = new BL_Shipping();
            EnvioRastreoResponseDto response  =  bL_Shipping.createPackage(envioRequestDto);
            return Ok(response);
            
        }


        [HttpPost("changeStatus")]

        public async Task<IActionResult> changeStatus(ChangeStatusPackageDTO changeStatus)
        {
            try
            {  
                SortCompra compra =  await  _compraService.getCompraByNroRastreo(changeStatus.trackingNumber);
                await _compraService.cambiarEstado(compra.Id, changeStatus.newStatusId);
                return Ok(new { Message = "El estado de la compra ha sido actualizado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}
