using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class PickUp
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Nombre { get; set; } = "";

        [Required]
        [Phone]
        public string Telefono = "";

        [Required]
        public string Foto = "";

        [Required]
        public double Lat = 0;

        [Required]
        public double Lng = 0;

        [Required]
        public int PlazoDiasPreparacion = 0;

        public int EmpresaId { get; set; } // Required foreign key property
        public Empresa Empresa { get; set; } = null!;

        public int DireccionId { get; set; }
        public Direccion Direccion { get; set; } = null!;

    }
}
