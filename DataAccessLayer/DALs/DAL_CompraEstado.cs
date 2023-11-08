using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public  class DAL_CompraEstado : IDAL_CompraEstado
    {
        private ApplicationDbContext _db;

        public DAL_CompraEstado(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CompraEstado> create(Compra compra, EstadoCompra estadoCompra) { 
            CompraEstado newCompraEstado = new CompraEstado { EstadoCompraId = estadoCompra.Id, EstadoActual = true, CompraId = compra.Id, compra = compra, EstadoCompra = estadoCompra };
            _db.ComprasEstados.Add(newCompraEstado);
            await _db.SaveChangesAsync();
            return newCompraEstado;
        }

       
    }
}
