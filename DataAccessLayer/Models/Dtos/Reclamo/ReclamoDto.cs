
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Usuario;

namespace DataAccessLayer.Models.Dtos.Reclamo
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
