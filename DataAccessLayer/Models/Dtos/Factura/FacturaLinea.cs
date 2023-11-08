using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Factura
{
    public  class FacturaLinea
    {
        public string fotoProducto { get; set; } = "";
        public string nombreProducto { get; set; } = "";

        public double precioUnitario { get; set; } = 0;

        public int cantidad { get; set; } = 0;

        public double subTotal { get; set; } = 0;
    }
}
