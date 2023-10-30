using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class PickUp
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Nombre { get; set; } = "";

        public bool Activo { get; set; } = true;


        [Required]
        [Phone]
        public string Telefono { get; set; } = "";

        [Required] 
        public string Foto { get; set; } = "";

        [Required]
        public double Lat { get; set; } = 0;

        [Required]
        public double Lng { get; set; } = 0;

        [Required]
        public int PlazoDiasPreparacion { get; set; } = 0;

        public int EmpresaId { get; set; } // Required foreign key property
        public Empresa Empresa { get; set; } = null!;

        public int DireccionId { get; set; }
        public Direccion Direccion { get; set; } = null!;

    }
}
