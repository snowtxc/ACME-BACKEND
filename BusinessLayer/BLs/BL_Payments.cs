using BusinessLayer.IBLs;
using DataAccessLayer.Enums;
using DataAccessLayer.Models.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Payments : IBL_Payments
    {

   
        public PaymentResponse ProcessPayment(PaymentRequestDto paymentRequest)
        {
            string cardNumber = paymentRequest.CardNumber;
            string lastCharacter = cardNumber.Substring(cardNumber.Length - 1);
            PaymentResponse response;
            switch (lastCharacter)
            {
                case "1":
                    response = PaymentResponse.OK;
                    break;
                case "2":
                    response = PaymentResponse.INCORRECT_VERIFICATION_CODE; 
                    break;
                case "3":
                    response = PaymentResponse.EXPIRED_CARD;
                    break;
                case "4":
                    response = PaymentResponse.INSUFFICIENT_BALANCE; 
                    break;
                default:
                    response = PaymentResponse.TIMEOUT; 
                    break;
                    
            }
            return response;
        }


    }
}
