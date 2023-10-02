using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class CategoriaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; } = null!;

        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

    }
}
