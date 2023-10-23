using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using acme_backend.Shared.Enums;

namespace acme_backend.Models
{
    public class Reclamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 


        [Required]
        public string Descripcion { get; set; } = "";
        public EstadoReclamo EstadoReclamo { get; set; }


        [Required]
        public int CompraId { get; set; }

        [Required]
        public Compra Compra { get; set; } = null!;
    }
}
 