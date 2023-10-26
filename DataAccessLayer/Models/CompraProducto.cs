using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class CompraProducto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; } = 0;

        [Required]
        public double PrecioUnitario { get; set; } = 0;


        public int CompraId { get; set; }

        public Compra Compra { get; set; } = null!;

        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
    }
}
