using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class ProductosRelacionados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Producto producto { get; set; } = null!;

        public int productoId { get; set; }

        public Producto productoRel { get; set; } = null!;

        public bool Activo { get; set; } = true;


        public int productoRelId { get; set; }

    }
}
