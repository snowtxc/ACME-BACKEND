using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Compra
    {
        Task  agregarEstado(int compraId, EstadoCompra nuevoEstadoCompra);
        Task<Compra> getById(int id);

        Task<List<Compra>> listByEmpresa(int empresaId);

        Task<List<Compra>> listByCliente(string clienteId);
    }
}
