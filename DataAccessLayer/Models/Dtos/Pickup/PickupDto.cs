namespace DataAccessLayer.Models.Dtos.Pickup
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

        public string CiudadNombre { get; set; } = string.Empty;

        public string DepartamentoNombre { get; set; } = string.Empty;

        public string Calle { get; set; } = string.Empty;

        public string NroPuerta { get; set; } = string.Empty;

        public string CalleEntre1 { get; set; } = string.Empty;

        public string CalleEntre2 { get; set; } = string.Empty;




    }

}
