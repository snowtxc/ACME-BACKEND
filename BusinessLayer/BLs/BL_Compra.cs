using AutoMapper;
using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Factura;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.BLs
{
    public class BL_Compra : IBL_Compra
    {
        private IDAL_Compra _compraIdal;
        private IDAL_CompraEstado  _compraEstadoIdal;
        private IDAL_EstadoCompra _estadoCompra;
        private IDAL_EnvioPaquete _envioPaquete;
        private readonly UserManager<Usuario> _userManager;

        private readonly IMapper _mapper;


        public BL_Compra(IDAL_Compra compraDal, IDAL_CompraEstado compraEstadoIdal, IDAL_EstadoCompra estadoCompra, IDAL_EnvioPaquete envioPaquete, IMapper mapper, UserManager<Usuario> _userManage)
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

        public async Task<List<SortCompra>> listByEmpresa(int empresaId)
        {
            List<Compra> compras = await _compraIdal.listByEmpresa(empresaId);
            List<SortCompra> result = new List<SortCompra>();
            foreach(Compra compra in compras)
            {
                result.Add(new SortCompra
                {
                    Id = compra.Id,
                    costoTotal = compra.CostoTotal,
                    user = new SortUserDto { Id = compra.Usuario.Id, Email = compra.Usuario.Email, Imagen = compra.Usuario.Imagen, Nombre = compra.Usuario.Nombre, Tel = compra.Usuario.Celular },
                    metodoEnvio = compra.MetodoEnvio.ToString(),
                    metodoPago = compra.MetodoPago.ToString(),
                    fecha = compra.Fecha,
                    estado = compra.ComprasEstados.Where(ce => ce.EstadoActual).FirstOrDefault().EstadoCompra.Nombre,
                    cantidadDeProductos = compra.ComprasProductos.Count
                }) ;
            }
            return result;
        }

        public async Task<CompraDto> getById(int id)
        {
            Compra compra =  await  _compraIdal.getById(id);
            if (compra == null)
            {
                throw new Exception("Compra no existe");
            }
            Usuario comprador = compra.Usuario;
            List<CompraLineaDto> lineas = new List<CompraLineaDto>();
            foreach(CompraProducto compraProducto  in compra.ComprasProductos) {
                   Producto producto = compraProducto.Producto;
                    List<ImagenList> imagenes = new List<ImagenList>();

                    int nroImage = 0;
                    foreach(ProductoFoto foto  in producto.Fotos)
                    {
                        imagenes.Add(new ImagenList { Id = foto.Id, Url = foto.Url });
                    }

                    ProductoLista productoDto = new ProductoLista
                    {
                        Id = producto.Id,
                        Nombre = producto.Titulo,
                        Descripcion = producto.Descripcion,
                        DocumentoPdf = producto.DocumentoPdf,
                        Precio = producto.Precio,
                        Imagenes = imagenes.ToArray()
                    };
                    lineas.Add(
                        new CompraLineaDto 
                        { PrecioUnitario = compraProducto.PrecioUnitario, 
                            Cantidad = compraProducto.Cantidad,
                            ProductoLista = productoDto , 
                            SubTotal = (compraProducto.Cantidad * compraProducto.PrecioUnitario)});
            }
            CompraDto compraInfo = new CompraDto
            {
                Id = compra.Id,
                CantidadDeProductos = compra.ComprasProductos.Count,
                Estado = compra.ComprasEstados.Where(ce => ce.EstadoActual).FirstOrDefault().EstadoCompra.Nombre,
                MetodoEnvio = compra.MetodoEnvio.ToString(),
                MetodoPago = compra.MetodoPago.ToString(),
                Fecha = compra.Fecha,
                Comprador = new UsuarioDto { Id = comprador.Id, Celular = comprador.Celular, Email = comprador.Email, Imagen = comprador.Imagen, Nombre = comprador.Nombre },
                CostoTotal = compra.CostoTotal,
                Lineas = lineas,
    
            };
            return compraInfo;
        }

        public async Task<List<CompraDto>> listByCliente(string clienteId)
        {
            List<Compra> compras = await this._compraIdal.listByCliente(clienteId);
            List<CompraDto> result = new List<CompraDto>();
            foreach(Compra compra in compras)
            {
                Usuario comprador = compra.Usuario;
                List<CompraLineaDto> lineas = new List<CompraLineaDto>();
                foreach (CompraProducto compraProducto in compra.ComprasProductos)
                {
                    Producto producto = compraProducto.Producto;
                    List<ImagenList> imagenes = new List<ImagenList>();

                    foreach (ProductoFoto foto in producto.Fotos)
                    {
                        imagenes.Add(new ImagenList { Id = foto.Id, Url = foto.Url });
                    }

                    ProductoLista productoDto = new ProductoLista
                    {
                        Id = producto.Id,
                        Nombre = producto.Titulo,
                        Descripcion = producto.Descripcion,
                        DocumentoPdf = producto.DocumentoPdf,
                        Precio = producto.Precio,
                        Imagenes = imagenes.ToArray()
                    };
                    lineas.Add(
                        new CompraLineaDto
                        {
                            PrecioUnitario = compraProducto.PrecioUnitario,
                            Cantidad = compraProducto.Cantidad,
                            ProductoLista = productoDto,
                            SubTotal = (compraProducto.Cantidad * compraProducto.PrecioUnitario)
                        });
                }
                result.Add(new CompraDto
                {
                    Id = compra.Id,
                    CantidadDeProductos = compra.ComprasProductos.Count,
                    Estado = compra.ComprasEstados.Where(ce => ce.EstadoActual).FirstOrDefault().EstadoCompra.Nombre,
                    MetodoEnvio = compra.MetodoEnvio.ToString(),
                    MetodoPago = compra.MetodoPago.ToString(),
                    Fecha = compra.Fecha,
                    Comprador = new UsuarioDto { Id = comprador.Id, Celular = comprador.Celular, Email = comprador.Email, Imagen = comprador.Imagen, Nombre = comprador.Nombre },
                    CostoTotal = compra.CostoTotal,
                    Lineas = lineas,

                });

            }
            return result;
        }
    }
}
