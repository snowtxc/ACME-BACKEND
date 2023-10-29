using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class CategoriaDestacada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = "";

        [Required]
        public string ImagenUrl { get; set; } = "";

        public int? CategoriaId { get; set; } = null;

        public Categoria Categoria { get; set; } = null!;

        public int? LookAndFeelId { get; set; } = null;

        public LookAndFeel LookAndFeel { get; set; } = null!;

    }
}
