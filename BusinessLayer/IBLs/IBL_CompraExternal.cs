using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_CompraExternal
    {
        public Task cambiarEstado(int compraId, int nuevoEstadoCompra);
        public Task<SortCompra> getCompraByNroRastreo(string trackingNumber);

    }
}
