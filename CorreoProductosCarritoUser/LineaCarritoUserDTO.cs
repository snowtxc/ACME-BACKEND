using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorreoProductosCarritoUser
{
    public class LineaCarritoUserDTO
    {
        public string userId { get; set; }
        public string userName { get; set; }

        public string userEmail { get; set; }

        public int empresaId { get; set; }

        public string empresaName { get; set; }

        public int cantidadProductos { get; set; }


    }
}
