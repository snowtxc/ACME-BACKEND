using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models.Dtos.Pickup
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
        public int EmpresaId;

        [Required]
        public int LocalidadId;

        [Required]
        public string Calle = string.Empty;

        public string NroPuerta = string.Empty;

        public string CalleEntre1 = string.Empty;

        public string CalleEntre2 = string.Empty;

        [Required]
        public int LocalidaId;

    }
}
