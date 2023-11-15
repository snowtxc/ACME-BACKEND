using DataAccessLayer.Models.Dtos.Usuario;

namespace DataAccessLayer.Models.Dtos.Compra
{
    public class SortCompra
    {

        public int Id { get; set; }

        public double costoTotal { get; set; } = 0;

        public string metodoPago { get; set; } = "";
         
        public string metodoEnvio { get; set; } = "";

        public DateTime fecha { get; set; }
        public SortUserDto user { get; set; } = null!;

        public String estado { get; set; } = "";

        public int cantidadDeProductos { get; set; } = 0;
      
    }
}
