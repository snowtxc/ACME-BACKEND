using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using acme_backend.Models.Dtos.Compra;
using acme_backend.Models.Dtos.Usuario;

namespace acme_backend.Models.Dtos.Reclamo
{
    public class ReclamoDto
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public string Estado { get; set; }

        public DateTime Fecha { get; set; }

        public SortCompra compra { get; set; }

        public SortUserDto usuario { get; set; }


    }
}
