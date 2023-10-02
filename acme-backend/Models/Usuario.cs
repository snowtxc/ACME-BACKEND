using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace acme_backend.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = "";

        [Required]
        [Phone]
        public string Celular { get; set; } = "";

        [Required]
        public string Imagen { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; } = "";

        public int RolId { get; set; } // Required foreign key property
        public Rol Rol { get; set; } 

        public int? EmpresaId { get; set; } = null;

        public Empresa? Empresa { get; set; } = null;


        public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

        public ICollection<LineaCarrito> LineasCarrito { get; set; } = new List<LineaCarrito>();


    }
}
