using DataAccessLayer.Models.Dtos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.Compra
{
    public  class CompraDto
    {
        public int Id { get; set; }

        public double CostoTotal { get; set; } = 0;

        public string MetodoPago { get; set; } = "";

        public string MetodoEnvio { get; set; } = "";

        public DateTime Fecha { get; set; }
        public UsuarioDto Comprador { get; set; }

        public String Estado { get; set; } = "";

        public int CantidadDeProductos { get; set; } = 0;

        public List<CompraLineaDto> Lineas { get; set; }
    }
}
