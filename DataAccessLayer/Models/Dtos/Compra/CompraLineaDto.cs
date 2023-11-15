using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Compra
{
    public class CompraLineaDto
    {
        public ProductoLista ProductoLista { get; set; }

        public int Cantidad { get; set; }

        public double PrecioUnitario { get; set; }

        public double SubTotal { get; set; }

    }
}
