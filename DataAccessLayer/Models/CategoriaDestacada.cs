using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class CategoriaDestacada
    {
        [Required]
        public string Nombre { get; set; } = "";

        [Required]
        public string ImagenUrl { get; set; } = "";

        [Required]
        public int? CategoriaId { get; set; } = null;

        [Required]
        public Categoria Categoria { get; set; } = null!;

        [Required]
        public int LookAndFeelId;

        [Required]
        public LookAndFeel LookAndFeel = null!;

    }
}
