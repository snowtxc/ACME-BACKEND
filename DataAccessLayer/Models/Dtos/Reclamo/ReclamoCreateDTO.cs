using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Reclamo
{
    public class ReclamoCreateDTO
    {

        public string Description { get; set; }
        public int compraId { get; set; }
    }
}
