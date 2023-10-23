using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace acme_backend.Models
{
    public class Usuario: IdentityUser
    {
        [Required]
        public string Nombre { get; set; } = "";

        [Required]
        [Phone]
        public string Celular { get; set; } = "";

        [Required]
        public string Imagen { get; set; } = "";

      


        public int? EmpresaId { get; set; } = null;

        public Empresa? Empresa { get; set; } = null;


        public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

        public ICollection<LineaCarrito> LineasCarrito { get; set; } = new List<LineaCarrito>();


        public ICollection<Compra> compras { get; set; } = new List<Compra>(); 








    }
}
