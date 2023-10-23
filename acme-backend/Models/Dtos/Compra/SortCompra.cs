namespace acme_backend.Models.Dtos.Compra
{
    public class SortCompra
    {

        public int Id { get; set; }

        public double costoTotal { get; set; } = 0;

        public string metodoPago { get; set; } = "";
         
        public string metodoEnvio { get; set; } = ""; 
      


    }
}
