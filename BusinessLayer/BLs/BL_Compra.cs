using AutoMapper;
using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Compra : IBL_Compra
    {
        private IDAL_Compra _compraIdal;
        private IDAL_CompraEstado  _compraEstadoIdal;
        private IDAL_EstadoCompra _estadoCompra;
        private IDAL_EnvioPaquete _envioPaquete;

        private readonly IMapper _mapper;


        public BL_Compra(IDAL_Compra compraDal, IDAL_CompraEstado compraEstadoIdal, IDAL_EstadoCompra estadoCompra, IDAL_EnvioPaquete envioPaquete, IMapper mapper)
        {
            _compraIdal = compraDal;
            _compraEstadoIdal = compraEstadoIdal;
            _estadoCompra = estadoCompra;
            _envioPaquete = envioPaquete;
            _mapper = mapper;
        }


        public async Task cambiarEstado(int compraId, int nuevoEstadoId)
        {
            Compra? compra =   await  _compraIdal.getById(compraId);
            if(compra == null)
            {
                throw new Exception("Compra no existe");
            }
            EstadoCompra? estado = await  _estadoCompra.getById(nuevoEstadoId);
            if(estado == null)
            {
                throw new Exception("El estado que se le intenta asignar a la compra no existe");
            }
            await _compraIdal.agregarEstado(compraId, estado);
            
        }

        public async Task<SortCompra> getCompraByNroRastreo(string trackingNumber)
        {
           EnvioPaquete? envioPaquete = await  _envioPaquete.getByNroRastreo(trackingNumber);
            if(envioPaquete == null)
            {
                throw new Exception("No existe un paquete asociado al codigo de seguimiento");
            }
            Compra compra = envioPaquete.Compra;
            SortCompra result = new SortCompra { Id = compra.Id, costoTotal = compra.CostoTotal, metodoEnvio = compra.MetodoEnvio.ToString(), metodoPago = compra.MetodoPago.ToString() };
            return result;
        }

        public async Task<FacturaDTO> getFacturaByCompraId(int id)
        {
            Compra? compra = await _compraIdal.getById(id);
            if(compra == null)
            {
                throw new Exception("La compra no existe");

            }
            Usuario cliente = compra.Usuario;
            Empresa empresa = compra.Empresa;
            List<FacturaLinea> lineas = new List<FacturaLinea>();
            foreach (CompraProducto compraProducto in compra.ComprasProductos)
            {
                Producto producto = compraProducto.Producto;
                string fotoProducto = producto.Fotos.First().Url;
                lineas.Add(new FacturaLinea
                {
                    cantidad = compraProducto.Cantidad,
                    fotoProducto = fotoProducto,
                    nombreProducto = producto.Titulo,
                    precioUnitario = compraProducto.PrecioUnitario,
                    subTotal = (compraProducto.PrecioUnitario * compraProducto.Cantidad)
                });
            }

            FacturaDTO factura = new FacturaDTO {
                nroFactura = new Random().Next(1, 6425),
                nombreCliente = cliente.Nombre, 
                celularCliente = cliente.Celular, 
                direccionCliente = "",
                nombreEmpresa = empresa.Nombre,
                correoEmpresa = empresa.Correo ,
                direccionEmpresa = empresa.Direccion,  
                telefonoEmpresa = empresa.Telefono,
                logo = empresa.Imagen,
                total = compra.CostoTotal,
                fecha = compra.Fecha.ToString("dd/MM/yy HH:mm"),
                lineas = lineas
            };
            return factura;

        }
    }
}
