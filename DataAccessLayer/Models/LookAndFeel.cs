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


        [Required]
        public string NavBarId { get; set; } = "";


        [Required]
        public int HomeId { get; set; }

        [Required]
        public string NombreSitio { get; set; } = "";

        [Required]
        public int EmpresaId;

        [Required]
        public Empresa Empresa = null!;



    }
}
