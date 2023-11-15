using BusinessLayer.IBLs;
using BusinessLayer.BLs;
using DataAccessLayer.Models.Dtos.Payment;
using Microsoft.AspNetCore.Mvc;

namespace PAYMENTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        [HttpPost("processPayment")]
        public IActionResult ProcessPayment(PaymentRequestDto paymentRequest)
        {
            try
            {
                IBL_Payments bL_Payments = new BL_Payments();
                return Ok(bL_Payments.ProcessPayment(paymentRequest));
            }
            catch (Exception ex)
            {
                return  BadRequest(ex.Message);

            }

        }



    }
}
