using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class EnvioPaquete
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CodigoSeguimiento { get; set; } = "";


        [Required]
        public DateTime FechaEstimadaEntrega;

        [Required]
        public int DireccionId { get; set; }

        public Direccion Direccion { get; set; } = null!;




    }
}
