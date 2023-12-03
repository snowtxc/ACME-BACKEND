
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class MetodosPagoPreferidosDTO
    {
        public int MercadoPago { get; set; } = 0;
        public int Tarjeta { get; set; } = 0;
        public int Wallet{ get; set; } = 0;
    }
}