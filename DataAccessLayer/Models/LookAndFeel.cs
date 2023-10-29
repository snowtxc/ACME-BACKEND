using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class LookAndFeel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string LogoUrl { get; set; } = "";

        public int? NavBarId { get; set; } = null;

        public int? HomeId { get; set; } = null;

        [Required]
        public string NombreSitio { get; set; } = "";

        [Required]
        public string ColorPrincipal { get; set; } = "";

        [Required]
        public string ColorSecundario { get; set; } = "";

        [Required]
        public string ColorFondo { get; set; } = "";

        public int? EmpresaId { get; set; } = null;

        public Empresa? Empresa { get; set; } = null!;

        public CategoriaDestacada? CategoriaDestacada { get; set; } = null;
    }
}
