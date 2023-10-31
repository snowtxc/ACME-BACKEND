using DataAccessLayer.Enums;
using DataAccessLayer.Models.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Payments
    {
        public PaymentResponse ProcessPayment(PaymentRequestDto paymentRequest);

    }
}
