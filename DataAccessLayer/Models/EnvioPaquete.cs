using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class EnvioPaquete
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CodigoSeguimiento { get; set; } = "";

        public bool Activo { get; set; } = true;



        [Required]
        public DateTime FechaEstimadaEntrega;

        [Required]
        public int DireccionId { get; set; }

        public Direccion Direccion { get; set; } = null!;

        [Required]
        public int CompraId { get; set; }
        public Compra Compra { get; set; } = null!;




    }
}
