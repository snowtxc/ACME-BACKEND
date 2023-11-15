using BusinessLayer.IBLs;
using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLayer.BLs
{
    public class BL_Carrito: IBL_Carrito
    {
        private IDAL_Carrito _carrito;

        public BL_Carrito(IDAL_Carrito carr)
        {
            _carrito = carr;
        }


        public Task<bool> agregarProductoCarrito(AgregarProductoCarritoDTO data, string userId)
        {
            return _carrito.agregarProductoCarrito(data, userId);
        }


        public Task<List<LineaCarritoDTO>> obtenerCarrito(int empresaId, string userId)
        {
            return _carrito.obtenerCarrito(empresaId, userId);
        }

        public Task<bool> borrarCarritoLinea(int carritoLinea, string userId)
        {
            return _carrito.borrarCarritoLinea(carritoLinea, userId);
        }

        public Task<CompraOKDTO> finalizarCarrito(FInalizarCarritoDTO data, string userId)
        {
            return _carrito.finalizarCarrito(data, userId);
        }

    }
}
