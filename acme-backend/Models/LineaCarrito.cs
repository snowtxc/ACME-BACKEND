using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class LineaCarrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; } = 0;

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        public Producto Producto { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;

    }
}
