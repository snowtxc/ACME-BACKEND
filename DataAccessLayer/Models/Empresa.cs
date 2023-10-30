using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = "";

        public bool Activo { get; set; } = true;


        [Required]
        public string Direccion { get; set; } = "";

        [Required]
        [Phone]
        public string Telefono { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = "";

        public string Imagen { get; set; } = "";

        public double CostoEnvio { get; set; } = 0;

        public string Wallet { get; set; } = "";

        [Required]
        public int LookAndFeelId;

        public LookAndFeel LookAndFeel = null!;

       public ICollection<Usuario> Usuarios { get; } = new List<Usuario>();

       public ICollection<PickUp> Pickups { get; } = new List<PickUp>();


       public ICollection<Categoria> Categorias { get; } = new List<Categoria>();

       public ICollection<Producto> Productos { get; } = new List<Producto>();
    }
}
