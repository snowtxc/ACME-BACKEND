using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class ProductoFoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } = "";

        public bool Activo { get; set; } = true;


        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

    }
}
