using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Compra : IDAL_Compra
    {

        private ApplicationDbContext _db;

        public DAL_Compra(ApplicationDbContext db)
        {
            _db = db;

        }

        public async Task<Compra?> getById(int id)
        {
            Compra? compra = await _db.Compras.Include(c => c.Empresa)
                .Include(c => c.Usuario).Include(c => c.ComprasProductos)
                .ThenInclude(cp => cp.Producto).ThenInclude(p => p.Fotos)
                .Include(c => c.ComprasEstados).ThenInclude(ce => ce.EstadoCompra).FirstOrDefaultAsync(c => c.Id == id);
            if(compra == null)
            {
                return null;
            }
            return compra;

        }

        public async Task agregarEstado(int compraId,  EstadoCompra nuevoEstado)
        {
            Compra? compra =  await this.getById(compraId);
            if(compra == null)
            {
                throw new Exception("No existe la compra");
            }
            CompraEstado? lastEstado = _db.ComprasEstados.Where(ce => ce.CompraId == compra.Id).Where(ce => ce.EstadoActual).OrderByDescending(e => e.CompraId).FirstOrDefault();
            if(lastEstado != null)
            {
                lastEstado.EstadoActual = false;
                _db.ComprasEstados.Update(lastEstado);
                await _db.SaveChangesAsync();
            }
            CompraEstado newCompraEstado = new CompraEstado { EstadoCompraId = nuevoEstado.Id, CompraId = compraId, EstadoActual = true, Activo = true };
            compra.ComprasEstados.Add(newCompraEstado);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Compra>> listByEmpresa(int empresaId)
        {
            List<Compra> compras =  await  _db.Compras.Where(c => c.EmpresaId == empresaId)
                .OrderByDescending(c => c.Fecha).Include(c => c.Usuario).Include(c => c.ComprasProductos)
                .Include(c => c.ComprasEstados).ThenInclude(ce => ce.EstadoCompra).ToListAsync();
            return compras;
        }

        public async Task<List<Compra>> listByCliente(string clienteId) { 
      
            List<Compra> compras = await _db.Compras.Where(c => c.UsuarioId == clienteId).OrderByDescending(c => c.Fecha).Include(c => c.Usuario).Include(c => c.ComprasProductos).ThenInclude(cp => cp.Producto).ThenInclude(p => p.Fotos)
                .Include(c => c.ComprasEstados).ThenInclude(ce => ce.EstadoCompra).ToListAsync();

            return compras;


        }
    }
}
