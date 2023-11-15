using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Db;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.TipoIVA;
using DataAccessLayer.Models.Dtos.Usuario;

namespace DataAccessLayer.IDALs
{
    public class DAL_Producto : IDAL_Producto
    {

        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;

        public DAL_Producto(ApplicationDbContext db, UserManager<Usuario> userManager)
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
            prod.CreatedAt = DateTime.Now.Date;
            var tipoIva = await _db.TiposIva.FindAsync(data.TipoIva);
            prod.TipoIva = tipoIva;

            foreach (string imageUrl in data.Imagenes)
            {
                ProductoFoto prodFoto = new ProductoFoto();
                prodFoto.Url = imageUrl;
                prodFoto.Producto = prod;
                await _db.ProductoFotos.AddAsync(prodFoto);
                prod.Fotos.Add(prodFoto);
            }
            await _db.Productos.AddAsync(prod);

            foreach (int prodId in data.ProductosRelacionados)
            {
                var producto = await _db.Productos.FindAsync(prodId);
                if (producto != null)
                {

                    ProductosRelacionados prodRel = new ProductosRelacionados();
                    prodRel.producto = prod;
                    prodRel.productoRel = producto;
                    await _db.ProductosRelacionados.AddAsync(prodRel);
                }
            }

            foreach (int catId in data.Categoria)
            {
                var categoria = await _db.Categorias.FindAsync(catId);
                if (categoria != null)
                {
                    CategoriaProducto catProd = new CategoriaProducto();
                    catProd.Producto = prod;
                    catProd.Categoria = categoria;
                    await _db.CategoriasProductos.AddAsync(catProd);
                }
            }


            await _db.SaveChangesAsync();

            // prod.LinkFicha = data.

            return true;
        }


        public async Task<bool> editProducto(EditProductoDTO data, string userId)
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


            var prod = await _db.Productos.Include((p) => p.Empresa).Include((p) => p.CategoriasProductos).ThenInclude((p) => p.Categoria).Where((p) => p.Empresa.Id == empresa.Id && p.Id == data.Id).FirstAsync();
            if (prod == null)
            {
                throw new Exception("Producto invalido");
            }

            prod.Descripcion = data.Descripcion;
            prod.DocumentoPdf = data.DocumentoPdf;
            prod.Titulo = data.Nombre;
            prod.Precio = data.Precio;
            prod.Empresa = empresa;
            var tipoIva = await _db.TiposIva.FindAsync(data.TipoIva);
            if (tipoIva != null)
            {
                prod.TipoIva = tipoIva;
            }

            foreach (string imageUrl in data.Imagenes)
            {
                ProductoFoto prodFoto = new ProductoFoto();
                prodFoto.Url = imageUrl;
                prodFoto.Producto = prod;
                await _db.ProductoFotos.AddAsync(prodFoto);
                prod.Fotos.Add(prodFoto);
            }

            foreach (int prodId in data.ProductosRelacionados)
            {
                var producto = await _db.Productos.FindAsync(prodId);
                if (producto != null)
                {
                    var existsRelation = await _db.ProductosRelacionados.Where((pr) => pr.productoId == prod.Id && pr.productoRelId == producto.Id).FirstOrDefaultAsync();
                    if (existsRelation == null)
                    {
                        ProductosRelacionados prodRel = new ProductosRelacionados();
                        prodRel.producto = prod;
                        prodRel.productoRel = producto;
                        await _db.ProductosRelacionados.AddAsync(prodRel);
                        await _db.SaveChangesAsync();
                    }

                }
            }

            foreach (int catId in data.Categoria)
            {
                var categoria = await _db.Categorias.FindAsync(catId);
                if (categoria != null)
                {
                    var existsCategoriaProd = await _db.CategoriasProductos.Include((p) => p.Producto).Include((p) => p.Categoria).Where((c) => c.Producto.Id == prod.Id && c.Categoria.Id == categoria.Id).FirstOrDefaultAsync();
                    if (existsCategoriaProd == null)
                    {
                        CategoriaProducto catProd = new CategoriaProducto();
                        catProd.Producto = prod;
                        catProd.Categoria = categoria;
                        await _db.CategoriasProductos.AddAsync(catProd);
                        await _db.SaveChangesAsync();
                    }
                }
            }

            foreach (CategoriaProducto cat in prod.CategoriasProductos)
            {
                var exists = data.Categoria.Contains(cat.Categoria.Id);
                if (exists == false)
                {
                    //borrar
                    _db.CategoriasProductos.Remove(cat);
                    await _db.SaveChangesAsync();
                }
            }
            var currentProductosRelacionados = await _db.ProductosRelacionados.Include((p) => p.producto).Include((p) => p.productoRel).Where((pr) => pr.productoId == prod.Id).ToListAsync();

            foreach (ProductosRelacionados prodRel in currentProductosRelacionados)
            {
                var exists = data.ProductosRelacionados.FirstOrDefault((item) => item == prodRel.productoRel.Id);
                var notexist = exists == 0;

                if (notexist)
                {
                    //borrar
                    _db.ProductosRelacionados.Remove(prodRel);
                    await _db.SaveChangesAsync();
                }
            }


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
                newProduct.Activo = p.Activo;
                newProduct.LinkFicha = p.LinkFicha;
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

                var calificaciones = _db.Calificaciones.Where((p) => p.ProductoId == newProduct.Id).ToList();

                var sumOfRates = 0;

                foreach (var calificacion in calificaciones)
                {
                    sumOfRates += calificacion.Rate;
                }

                if (calificaciones.Count() > 0)
                {
                    newProduct.CantCalificaciones = calificaciones.Count();
                    newProduct.Rate = sumOfRates / calificaciones.Count();
                }

                lista.Add(newProduct);
            });

            return lista;
        }

        public async Task<List<ProductoLista>> listarProductos(int empresaId)
        {
            var lista = new List<ProductoLista>();
            var empresa = await _db.Empresas.FindAsync(empresaId);
            if (empresa == null)
            {
                throw new Exception("Empresa invalida");
            }

            var productos = _db.Productos.Include((p) => p.CategoriasProductos).ThenInclude((p) => p.Categoria).Include((p) => p.TipoIva).Include((p) => p.Fotos).Where((p) => p.Empresa.Id == empresaId).ToList();


            productos.ForEach(p =>
            {
                ProductoLista newProduct = new ProductoLista();
                newProduct.DocumentoPdf = p.DocumentoPdf;
                newProduct.Id = p.Id;
                newProduct.Descripcion = p.Descripcion;
                newProduct.Nombre = p.Titulo;
                newProduct.Precio = p.Precio;
                newProduct.Activo = p.Activo;
                newProduct.CreatedAt = p.CreatedAt;
                newProduct.LinkFicha = p.LinkFicha;
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

                var calificaciones = _db.Calificaciones.Where((p) => p.ProductoId == newProduct.Id).ToList();

                var sumOfRates = 0;

                foreach (var calificacion in calificaciones)
                {
                    sumOfRates += calificacion.Rate;
                }

                if (calificaciones.Count() > 0)
                {
                    newProduct.CantCalificaciones = calificaciones.Count();
                    newProduct.Rate = sumOfRates / calificaciones.Count();
                }

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
            var p = await _db.Productos.Include((p) => p.CategoriasProductos).ThenInclude((p) => p.Categoria).Include((p) => p.TipoIva).Include((p) => p.Fotos).FirstOrDefaultAsync((p) => p.Id == productoId);

            if (p != null)
            {
                ProductoLista newProduct = new ProductoLista();
                newProduct.DocumentoPdf = p.DocumentoPdf;
                newProduct.Id = p.Id;
                newProduct.Descripcion = p.Descripcion;
                newProduct.Nombre = p.Titulo;
                newProduct.Precio = p.Precio;
                newProduct.LinkFicha = p.LinkFicha;
                newProduct.Activo = p.Activo;
                newProduct.CreatedAt = p.CreatedAt;

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

                var productosRelacionados = _db.ProductosRelacionados.Include((p) => p.productoRel).ThenInclude((p) => p.Fotos).Where((p) => p.productoId == newProduct.Id).ToList();

                foreach (var prodRel in productosRelacionados)
                {
                    var productoRel = prodRel.productoRel;
                    ProductoLista prod = new ProductoLista();
                    prod.Activo = productoRel.Activo;
                    prod.DocumentoPdf = productoRel.DocumentoPdf;
                    prod.Id = productoRel.Id;
                    prod.Descripcion = productoRel.Descripcion;
                    prod.Nombre = productoRel.Titulo;
                    prod.Precio = productoRel.Precio;
                    prod.CreatedAt = productoRel.CreatedAt;
                    prod.Imagenes = productoRel.Fotos.Select((imagen) => new ImagenList
                    {
                        Id = imagen.Id,
                        Url = imagen.Url,
                    }).ToArray();

                    newProduct.ProductosRelacionados.Add(prod);
                }

                var calificaciones = _db.Calificaciones.Include((p) => p.Usuario).Where((p) => p.ProductoId == newProduct.Id).ToList();

                var sumOfRates = 0;

                List<CalificacionItemList> allCalificaciones = new List<CalificacionItemList>();

                foreach (var calificacion in calificaciones)
                {
                    sumOfRates += calificacion.Rate;
                    var item = new CalificacionItemList();
                    item.UsuarioNombre = calificacion.Usuario.Nombre;
                    item.UsuarioImagen = calificacion.Usuario.Imagen;
                    item.Descripcion = calificacion.Comentario;
                    item.Rate = calificacion.Rate;
                    item.CalificacionId = calificacion.Id;
                    allCalificaciones.Add(item);
                }

                if (calificaciones.Count() > 0)
                {
                    newProduct.CantCalificaciones = calificaciones.Count();
                    newProduct.Rate = sumOfRates / calificaciones.Count();
                    newProduct.calificaciones = allCalificaciones;
                }





                return newProduct;
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> deshabilitarProducto(string userId, int productoId)
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
            var p = await _db.Productos.Include((p) => p.Empresa).Where((p) => p.Empresa.Id == empresaId && p.Id == productoId).FirstAsync();
            if (p == null)
            {
                throw new Exception("Error al borrar producto, el producto es invalido o no tienes permisos suficientes");
            }

            p.Activo = false;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductoLista>> obtenerProductosRelacionados(string userId, int[] productosIds)
        {
            Random random = new Random();
            var lista = new List<ProductoLista>();
            var userInfo = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }

            foreach (var prodId in productosIds)
            {
                var producto = await _db.Productos.Include((p) => p.Empresa).Include((p) => p.CategoriasProductos).ThenInclude((p) => p.Categoria).FirstOrDefaultAsync((p) => p.Id == prodId);

                if (producto != null)
                {
                    var productosRelacionados = _db.ProductosRelacionados.Include((p) => p.productoRel).ThenInclude((p) => p.Fotos).Where((p) => p.productoId == producto.Id).Take(2).OrderBy(o => Guid.NewGuid()).ToList();
                    var counter = 0;
                    foreach (var prodRel in productosRelacionados)
                    {
                        var productoRel = prodRel.productoRel;
                        ProductoLista prod = new ProductoLista();
                        prod.Activo = productoRel.Activo;
                        prod.DocumentoPdf = productoRel.DocumentoPdf;
                        prod.Id = productoRel.Id;
                        prod.Descripcion = productoRel.Descripcion;
                        prod.Nombre = productoRel.Titulo;
                        prod.Precio = productoRel.Precio;
                        prod.CreatedAt = productoRel.CreatedAt;
                        prod.Imagenes = productoRel.Fotos.Select((imagen) => new ImagenList
                        {
                            Id = imagen.Id,
                            Url = imagen.Url,
                        }).ToArray();

                        lista.Add(prod);
                        counter++;
                    }

                    foreach (var catRel in producto.CategoriasProductos)
                    {
                        var categoriaId = catRel.ProductoId;

                        var categoriasRelacionadasOfThisCat = await _db.CategoriaRelacionadas.Where((cat) => cat.Categoria.Id == categoriaId || cat.CategoriaRel.Id == categoriaId).ToListAsync();

                        foreach (var rel in categoriasRelacionadasOfThisCat)
                        {
                            var categoriaRelId = rel.CategoriaId;
                            var catProdRel = await _db.CategoriasProductos.Include((pr) => pr.Producto).ThenInclude((p) => p.Fotos).Where((pr) => pr.ProductoId == categoriaRelId).OrderBy(o => Guid.NewGuid()).ToListAsync();
                            var randomProdsOrder = catProdRel.OrderBy(x => random.Next()).Take(2).ToList();

                            foreach (var prodRel in randomProdsOrder)
                            {
                                var prod = prodRel.Producto;

                                var exists = lista.Find((item) => item.Id == prod.Id);
                                if (exists == null)
                                {
                                    // add to list
                                    ProductoLista prodToAdd = new ProductoLista();
                                    prodToAdd.Activo = prod.Activo;
                                    prodToAdd.DocumentoPdf = prod.DocumentoPdf;
                                    prodToAdd.Id = prod.Id;
                                    prodToAdd.Descripcion = prod.Descripcion;
                                    prodToAdd.Nombre = prod.Titulo;
                                    prodToAdd.Precio = prod.Precio;
                                    prodToAdd.CreatedAt = prod.CreatedAt;
                                    prodToAdd.Imagenes = prod.Fotos.Select((imagen) => new ImagenList
                                    {
                                        Id = imagen.Id,
                                        Url = imagen.Url,
                                    }).ToArray();

                                    lista.Add(prodToAdd);
                                }
                            }
                        }
                    }
                }
            }


            return lista;
        }
        public async Task calificarProducto(string userId, CreateCalificacionDTO calificacionDto)
        {
            var userInfo = await _db.Usuarios
           .Include(u => u.Empresa)
           .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario inválido");
            }

            var producto = await _db.Productos
               .FirstOrDefaultAsync(p => p.Id == calificacionDto.ProductoId);

            if (userInfo == null)
            {
                throw new Exception("Producto inválido");
            }

            var existsCalificacion = _db.Calificaciones
                .Include((p) => p.Usuario)
                .Where((p) => p.ProductoId == calificacionDto.ProductoId)
                .Where((p) => p.UsuarioId == userId)
                .ToList();

            if (existsCalificacion.Count > 0)
            {
                throw new Exception("El usuario ya cuenta con una calificación existente para este producto.");
            }

            Calificacion newCalificacion = new Calificacion()
            {
                UsuarioId = userId,
                ProductoId = calificacionDto.ProductoId,
                Comentario = calificacionDto.Comentario,
                Rate = calificacionDto.Rate,
            };

            await _db.Calificaciones.AddAsync(newCalificacion);
            await _db.SaveChangesAsync();
        }
    }
}