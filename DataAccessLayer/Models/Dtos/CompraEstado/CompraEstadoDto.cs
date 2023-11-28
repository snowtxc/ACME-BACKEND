using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos.CompraEstado
{
    public class CompraEstadoDto
    {
        public DateTime? Fecha { get; set; } = null;

        public string Estado { get; set; }

        public int EstadoId { get; set; }

        public bool Completado { get; set; }

      
    }
}
