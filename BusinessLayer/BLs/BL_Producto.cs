using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Producto: IBL_Producto
    {
        private IDAL_Producto _producto;

        public BL_Producto(IDAL_Producto prod)
        {
            _producto = prod;
        }

        public Task<bool> createProduct(CrearProductoDTO data, string userId)
        {
            return _producto.createProduct(data, userId);
        }
        public Task<bool> editProducto(EditProductoDTO data, string userId)
        {
            return _producto.editProducto(data, userId);
        }
        public Task<List<ProductoLista>> listarProductosDeMiEmpresa(string userId)
        {
            return _producto.listarProductosDeMiEmpresa(userId);
        }
        public Task<List<ProductoLista>> listarProductos(int empresaId)
        {
            return _producto.listarProductos(empresaId);
        }
        public Task<ProductoLista> obtenerProductoById(string userId, int productoId)
        {
            return _producto.obtenerProductoById(userId, productoId);
        }

        public Task<List<ProductoLista>> obtenerProductosRelacionados(string userId, int[] productosIds)
        {
            return _producto.obtenerProductosRelacionados(userId, productosIds);
        }
        public Task<bool> deshabilitarProducto(string userId, int productoId)
        {
            return _producto.deshabilitarProducto(userId, productoId);
        }
    }
}
