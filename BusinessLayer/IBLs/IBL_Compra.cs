
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Compra
    {
        public  Task<FacturaDTO> getFacturaByCompraId(int id);
        public Task cambiarEstado(int compraId, int nuevoEstadoCompra);
        public Task<SortCompra> getCompraByNroRastreo(string trackingNumber);

        public Task<List<SortCompra>> listByEmpresa(int empresa);

        public Task<CompraDto> getById(int id);

        public Task<List<CompraDto>> listByCliente(string clienteId);

    }
}
