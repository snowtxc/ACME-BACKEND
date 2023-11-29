using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models;
using BusinessLayer.IBLs;
using AutoMapper;
using DataAccessLayer.IDALs;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.BLs
{
    public class Bl_CompraExternal: IBL_CompraExternal
    {
        private IDAL_Compra _compraIdal;
        private IDAL_EstadoCompra _estadoCompra;
        private IDAL_EnvioPaquete _envioPaquete;

        public Bl_CompraExternal(IDAL_Compra compraDal)
        {
            _compraIdal = compraDal;
        }
        public async Task cambiarEstado(int compraId, int nuevoEstadoId)
        {
            Compra? compra = await _compraIdal.getById(compraId);
            if (compra == null)
            {
                throw new Exception("Compra no existe");
            }
            EstadoCompra? estado = await _estadoCompra.getById(nuevoEstadoId);
            if (estado == null)
            {
                throw new Exception("El estado que se le intenta asignar a la compra no existe");
            }
            await _compraIdal.agregarEstado(compraId, estado);

        }

        public async Task<SortCompra> getCompraByNroRastreo(string trackingNumber)
        {
            EnvioPaquete? envioPaquete = await _envioPaquete.getByNroRastreo(trackingNumber);
            if (envioPaquete == null)
            {
                throw new Exception("No existe un paquete asociado al codigo de seguimiento");
            }
            Compra compra = envioPaquete.Compra;
            SortCompra result = new SortCompra { Id = compra.Id, costoTotal = compra.CostoTotal, metodoEnvio = compra.MetodoEnvio.ToString(), metodoPago = compra.MetodoPago.ToString() };
            return result;
        }

    }
}
