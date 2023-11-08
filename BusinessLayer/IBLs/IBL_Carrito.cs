﻿using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Carrito
    {

        Task<bool> agregarProductoCarrito(AgregarProductoCarritoDTO data, string userId);

        Task<List<LineaCarritoDTO>> obtenerCarrito(int empresaId, string userId);


        Task<bool> borrarCarritoLinea(int carritoLinea, string userId);

    }
}