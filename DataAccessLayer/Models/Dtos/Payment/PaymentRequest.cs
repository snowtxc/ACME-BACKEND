
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos.Payment
{
    public class PaymentRequestDto
    {

        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public string ExpiryDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Amount { get; set; }
    }
}
