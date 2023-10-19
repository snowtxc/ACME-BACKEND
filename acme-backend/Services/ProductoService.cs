using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Models.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class ProductoService
    {

        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;

        public ProductoService(ApplicationDbContext db, UserManager<Usuario> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<bool> createProduct(CrearProductoDTO data, string userId)
        {

            var lista = new List<ProductoLista>();
            var userInfo = await _userManager.Users
           .Include(u => u.Empresa)
           .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            if (userInfo.Empresa == null)
            {
                throw new Exception("Empresa invalida");
            }

            var empresa = userInfo.Empresa;

            Producto prod = new Producto();
            prod.Descripcion = data.Descripcion;
            prod.DocumentoPdf = data.DocumentoPdf;
            prod.Titulo = data.Nombre;
            prod.Precio = data.Precio;
            prod.Empresa = empresa;
            var tipoIva = await _db.TiposIva.FindAsync(data.TipoIva);
            prod.TipoIva = tipoIva;
            foreach (int catId in data.Categoria)
            {
                var categoria = await _db.Categorias.FindAsync(catId);
                if (categoria != null)
                {
                    CategoriaProducto catProd = new CategoriaProducto();
                    catProd.Producto = prod;
                    catProd.Categoria = categoria;
                    await _db.CategoriasProductos.AddAsync(catProd);
                    prod.CategoriasProductos.Add(catProd);
                }
            }
            foreach (string imageUrl in data.Imagenes)
            {
                ProductoFoto prodFoto = new ProductoFoto();
                prodFoto.Url = imageUrl;
                prodFoto.Producto = prod;
                await _db.ProductoFotos.AddAsync(prodFoto);
                prod.Fotos.Add(prodFoto);
            }


            await _db.Productos.AddAsync(prod);
            await _db.SaveChangesAsync();

            // prod.LinkFicha = data.

            return true;
        }

        public async Task<List<ProductoLista>> listarProductosDeMiEmpresa(string userId)
        {

            var lista = new List<ProductoLista>();
            var userInfo = await _userManager.Users
           .Include(u => u.Empresa)
           .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            if (userInfo.Empresa == null)
            {
                throw new Exception("Empresa invalida");
            }

            var empresaId = userInfo.Empresa.Id;
            var productos = _db.Productos.Include((p) => p.CategoriasProductos).ThenInclude((p) => p.Categoria).Include((p) => p.TipoIva).Include((p) => p.Fotos).Where((p) => p.Empresa.Id == empresaId).ToList();


            productos.ForEach(p =>
            {
                ProductoLista newProduct = new ProductoLista();
                newProduct.DocumentoPdf = p.DocumentoPdf;
                newProduct.Id = p.Id;
                newProduct.Descripcion = p.Descripcion;
                newProduct.Nombre = p.Titulo;
                newProduct.Precio = p.Precio;
                newProduct.Imagenes = p.Fotos.Select((imagen) => new ImagenList
                {
                    Id = imagen.Id,
                    Url = imagen.Url,
                }).ToArray();
                newProduct.Categorias = p.CategoriasProductos.Select((categoria) => new CategoriaLista
                {
                    Nombre = categoria.Categoria.Nombre,
                    Id = categoria.Categoria.Id,
                }).ToArray();
                newProduct.TipoIva = new TipoIvaList
                {
                    Id = p.TipoIva.Id,
                    Nombre = p.TipoIva.Nombre,
                    Porcentaje = p.TipoIva.Porcentaje,
                };

                lista.Add(newProduct);
            });

            return lista;
        }

        public async Task<ProductoLista> obtenerProductoById(string userId, int productoId)
        {

            var userInfo = await _userManager.Users
           .Include(u => u.Empresa)
           .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            if (userInfo.Empresa == null)
            {
                throw new Exception("Empresa invalida");
            }
            var empresaId = userInfo.Empresa.Id;
            var p = await _db.Productos.Include((p) => p.CategoriasProductos).ThenInclude((p) => p.Categoria).Include((p) => p.TipoIva).Include((p) => p.Fotos).FirstOrDefaultAsync((p) => p.Id == productoId);

            if (p != null)
            {
                ProductoLista newProduct = new ProductoLista();
                newProduct.DocumentoPdf = p.DocumentoPdf;
                newProduct.Id = p.Id;
                newProduct.Descripcion = p.Descripcion;
                newProduct.Nombre = p.Titulo;
                newProduct.Precio = p.Precio;
                newProduct.Imagenes = p.Fotos.Select((imagen) => new ImagenList
                {
                    Id = imagen.Id,
                    Url = imagen.Url,
                }).ToArray();
                newProduct.Categorias = p.CategoriasProductos.Select((categoria) => new CategoriaLista
                {
                    Nombre = categoria.Categoria.Nombre,
                    Id = categoria.Categoria.Id,
                }).ToArray();
                newProduct.TipoIva = new TipoIvaList
                {
                    Id = p.TipoIva.Id,
                    Nombre = p.TipoIva.Nombre,
                    Porcentaje = p.TipoIva.Porcentaje,
                };
                return newProduct;
            } else
            {
                return null;
            }
        }
    }
}
