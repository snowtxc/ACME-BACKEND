using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos.Pickup
{
    public class PickupCreateDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;


        [Required]
        public string Telefono { get; set; } = string.Empty;


        [Required]
        public string Foto { get; set; } = string.Empty;


        [Required]
        public double Lat { get; set; } = 0;

        [Required]
        public double Lng { get; set; } = 0;

        [Required]
        public int PlazosDiasPreparacion { get; set; } = 0;


        [Required]
        public int LocalidadId { get; set; }

        [Required]
        public string Calle { get; set; } = string.Empty;
         
        public string NroPuerta { get; set; } = string.Empty;

        public string CalleEntre1 { get; set; } = string.Empty;

        public string CalleEntre2 { get; set; } = string.Empty;


    }
}
