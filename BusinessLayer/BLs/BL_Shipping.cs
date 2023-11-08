using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Envio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Shipping : IBL_Shipping
    {
        
        public EnvioRastreoResponseDto createPackage(EnvioRequestDto data)
        {

            double minutesToSum = new Random().Next(1440, 10080);    //minutos a sumar , entre 1440 minutos equivalente a 1 dia y 10080 equivalente a 7 dias
            DateTime arrivalDate = DateTime.Now.AddMinutes(minutesToSum);
            string trackingNumber = Guid.NewGuid().ToString();
            double costo = 0;
            int  days = (int)Math.Round(minutesToSum / 1440);
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
            return new EnvioRastreoResponseDto
            {
                trackingNumber = trackingNumber,
                arrivalDate = arrivalDate,
                Message = "Se ha generado un nuevo paquete exitosamente",
                Costo = costo,
                DireccionEnvio = data.NroPuerta + " " + data.Calle + " " + data.Ciudad + " " + data.Departamento
            };
        }

        public void changeStatus(string trackingNumber, int newStatusId)
        {
            throw new NotImplementedException();
        }



    }
}
