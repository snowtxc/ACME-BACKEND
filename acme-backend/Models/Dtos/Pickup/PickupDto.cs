namespace acme_backend.Models.Dtos.Pickup
{
    public class PickupDto
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;


        public string Telefono { get; set; } = string.Empty;


        public string Foto { get; set; } = string.Empty;


        public double Lat { get; set; } = 0;

        public double Lng { get; set; } = 0;

        public int PlazosDiasPreparacion { get; set; } = 0;
    }

}
