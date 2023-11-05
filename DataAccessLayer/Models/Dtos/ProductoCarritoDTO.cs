using DataAccessLayer.Models.Dtos.TipoIVA;
using DataAccessLayer.Models.Dtos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos
{
    public class ProductoCarritoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public bool Activo { get; set; }


        public TipoIvaList TipoIva { get; set; }

        public ImagenList[] Fotos { get; set; }
    }
}
