using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_CompraEstado
    {
        public  Task<CompraEstado> create(Compra compra , EstadoCompra estadoCompra);
    }
}
