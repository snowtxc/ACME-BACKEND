using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Envio
{
    public class EnvioRastreoResponseDto
    {

        public string Message { get; set; }

        public string trackingNumber { get; set; }

        public DateTime arrivalDate { get; set; }

        public double Costo { get; set; }

        public string DireccionEnvio { get; set; }

    }
}
