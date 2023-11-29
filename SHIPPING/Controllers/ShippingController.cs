using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Envio;
using Microsoft.AspNetCore.Mvc;
using SHIPPING.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SHIPPING.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : Controller
    {

        public ShippingController() {
        }

        [HttpPost("createPackage")]

        public IActionResult createPackage(EnvioRequestDto envioRequestDto)
        {

            double minutesToSum = new Random().Next(1440, 10080);    //minutos a sumar , entre 1440 minutos equivalente a 1 dia y 10080 equivalente a 7 dias
            DateTime arrivalDate = DateTime.Now.AddMinutes(minutesToSum);
            string trackingNumber = Guid.NewGuid().ToString();
            double costo = 0;
            int days = (int)Math.Round(minutesToSum / 1440);
            switch (days)
            {
                case 1:
                    costo = 70.0;
                    break;
                case 2:
                    costo = 60.0;
                    break;
                case 3:
                    costo = 50.0;
                    break;
                case 4:
                    costo = 40.0;
                    break;
                case 5:
                    costo = 30.0;
                    break;
                case 6:
                    costo = 20.0;
                    break;
                case 7:
                    costo = 10.0;
                    break;

            }
            return Ok(new EnvioRastreoResponseDto
            {
                trackingNumber = trackingNumber,
                arrivalDate = arrivalDate,
                Message = "Se ha generado un nuevo paquete exitosamente",
                Costo = costo,
                DireccionEnvio = envioRequestDto.NroPuerta + " " + envioRequestDto.Calle + " " + envioRequestDto.Ciudad + " " + envioRequestDto.Departamento
            });

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
