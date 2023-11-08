using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class EstadoCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

          
        [Required]
        public string Nombre { get; set; } = "";

        public bool Activo { get; set; } = true;


        public List<CompraEstado> ComprasEstados { get; set; } = new();
    }
}
