using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class Reclamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 


        [Required]
        public string Descripcion { get; set; } = "";

        [Required]
        public int CompraId { get; set; }

        [Required]
        public Compra Compra = null!;
    }
}
 