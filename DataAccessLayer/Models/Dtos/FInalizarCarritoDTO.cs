using DataAccessLayer.Models.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos
{
    public class FInalizarCarritoDTO
    {
        public int MetodoEnvio { get; set; } = 0;

        public int MetodoPago { get; set; } = 0;

        public int EmpresaId { get; set; }

        public int? DireccionSeleccionadaId { get; set; } = 0;

        public PaymentRequestDto? PaymentInfo { get; set; } = null;

        public string wallet {  get; set; } = "";
    }
}
