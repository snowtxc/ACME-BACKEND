using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class LineaCarrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; } = 0;


        public bool Activo { get; set; } = true;

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [Column]
        public bool correoEnviado { get; set; } = false;

        [Column]
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;

        public Producto Producto { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;

    }
}
