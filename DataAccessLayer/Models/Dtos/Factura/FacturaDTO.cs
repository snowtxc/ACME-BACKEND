using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Factura
{
    public class FacturaDTO
    {

        public int nroFactura { get; set; } = 0;

        public string logo { get; set; } = "";
        public string nombreEmpresa { get; set; } = "";

        public string direccionEmpresa { get; set; } = "";

        public string telefonoEmpresa { get; set; } = "";

        public string correoEmpresa { get; set; } = "";

        public string fecha { get; set; } = "";

        public string nombreCliente { get; set; } = "";
        public string direccionCliente { get; set; } = "";
        public string celularCliente { get; set; } = "";


        public double total { get; set; } = 0;

        public  List<FacturaLinea> lineas { get; set; }
    }
}
