using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Envio
{
    public class EnvioRequestDto
    {
        public string Calle { get; set; }

        public string Ciudad { get; set; }

        public string Departamento { get; set; }

        public int CantidadDeProductos { get; set; }

        public string NroPuerta { get; set; }
    }
}
