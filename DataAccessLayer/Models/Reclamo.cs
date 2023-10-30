using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;

namespace DataAccessLayer.Models
{
    public class Reclamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public bool Activo { get; set; } = true;


        [Required]
        public string Descripcion { get; set; } = "";
        public EstadoReclamo EstadoReclamo { get; set; }


        [Required]
        public int CompraId { get; set; }

        [Required]
        public Compra Compra { get; set; } = null!;
    }
}
 