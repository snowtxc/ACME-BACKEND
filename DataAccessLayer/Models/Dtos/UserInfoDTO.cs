using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class UserInfoDTO
    {
        public string Id { get; set; } = "";

        public string Nombre { get; set; } = "";

        public string Email { get; set; } = "";

        public string Celular { get; set; } = "";

        public string Imagen { get; set; } = "";

        public IList<string> Roles { get; set; } = new List<string>();


        public int? EmpresaId { get; set; } = null;

        //public Empresa? Empresa { get; set; } = null;


        //public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

        //public ICollection<LineaCarrito> LineasCarrito { get; set; } = new List<LineaCarrito>();

    }
}
