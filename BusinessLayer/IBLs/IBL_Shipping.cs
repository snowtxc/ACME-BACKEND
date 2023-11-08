using DataAccessLayer.Models.Dtos.Envio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public  interface IBL_Shipping
    {
        public EnvioRastreoResponseDto createPackage(EnvioRequestDto request);
        public void changeStatus(string trackingNumber, int newStatusId);

    }
}
