
using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Producto
    {
        Task<bool> createProduct(CrearProductoDTO data, string userId);
        Task<bool> editProducto(EditProductoDTO data, string userId);
        Task<List<ProductoLista>> listarProductosDeMiEmpresa(string userId);
        Task<List<ProductoLista>> listarProductos(int empresaId, string query);
        Task<ProductoLista> obtenerProductoById(string userId, int productoId);
        Task<bool> deshabilitarProducto(string userId, int productoId);
        Task calificarProducto(string userId, CreateCalificacionDTO calificacionDto);
        Task<List<ProductoLista>> obtenerProductosRelacionados(string userId, int[] productosIds);
    }
}
