using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Ciudad;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Carrito: IDAL_Carrito
    {
        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;

        public DAL_Carrito(ApplicationDbContext db, UserManager<Usuario> userManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> agregarProductoCarrito(AgregarProductoCarritoDTO data, string userId)
        {
            try
            {
                var userInfo = await _userManager.Users
                .Include(u => u.Empresa)
                .FirstOrDefaultAsync(u => u.Id == userId);

                // TODO: agregar validacion por si el usuario no es un vendedor
                if (userInfo == null)
                {
                    throw new Exception("Usuario invalido");
                }
                var producto = await _db.Productos.Include((p) => p.Empresa).FirstOrDefaultAsync((p) => p.Id == data.ProductoId);
                if (producto == null)
                {
                    throw new Exception("Erorr , producto invalido");
                }
                var empresaId = producto.Empresa.Id;

                var existsOnCarrito = await _db.LineasCarrito.Include((p) => p.Producto).Include((p) => p.Usuario).Where((p) => p.Producto.Id == producto.Id && p.Usuario.Id == userInfo.Id).FirstOrDefaultAsync();
                if (existsOnCarrito != null)
                {
                    existsOnCarrito.Cantidad = data.Cantidad;
                    _db.LineasCarrito.Update(existsOnCarrito);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var lineaCarrito = new LineaCarrito();
                    lineaCarrito.Usuario = userInfo;
                    lineaCarrito.Producto = producto;
                    lineaCarrito.Cantidad = data.Cantidad;
                    _db.LineasCarrito.Add(lineaCarrito);
                    await _db.SaveChangesAsync();
                }
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<List<LineaCarritoDTO>> obtenerCarrito(int empresaId, string userId)
        {
            var lineasProducto = await _db.LineasCarrito
                .Include((p) => p.Producto)
                    .ThenInclude((c) => c.TipoIva)
                    .Include((c) => c.Producto.Empresa)
                    .Include((c) => c.Producto.Fotos)
                .Where((l) => l.Producto.Empresa.Id == empresaId).ToListAsync();

            if (lineasProducto != null)
            {
                return _mapper.Map<List<LineaCarritoDTO>>(lineasProducto);
            } else
            {
                return new List<LineaCarritoDTO> { };
            }

        }

        public async Task<bool> borrarCarritoLinea(int carritoLinea, string userId)
        {
            var linea = await _db.LineasCarrito.Include((p) => p.Usuario).Where((l) => l.Id == carritoLinea && l.Usuario.Id == userId).FirstOrDefaultAsync();
            if (linea == null)
            {
                throw new Exception("Error al borrar producto del carrito");
            }
            _db.LineasCarrito.Remove(linea);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
