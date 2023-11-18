using DataAccessLayer.Models.Dtos;

namespace BusinessLayer.IBLs
{
    public interface IBL_Producto
    {
        public Task<bool> createProduct(CrearProductoDTO data, string userId);
        public Task<bool> editProducto(EditProductoDTO data, string userId);
        public Task<List<ProductoLista>> listarProductosDeMiEmpresa(string userId);
        public Task<List<ProductoLista>> listarProductos(int empresaId, string query);
        public Task<ProductoLista> obtenerProductoById(string userId, int productoId);
        public Task<bool> deshabilitarProducto(string userId, int productoId);
        public Task calificarProducto(string userId, CreateCalificacionDTO calificacionDto);
        public Task<List<ProductoLista>> obtenerProductosRelacionados(string userId, int[] productosIds);
    }
}
