using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos
{
    public class LineaCarritoDTO
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        public ProductoCarritoDTO Producto { get; set; }
    }
}
