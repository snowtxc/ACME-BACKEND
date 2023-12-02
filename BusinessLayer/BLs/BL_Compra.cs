using AutoMapper;
using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Factura;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DALs;
using DataAccessLayer.Models.Dtos.Estado;
using DataAccessLayer.Models.Dtos.CompraEstado;
using DataAccessLayer.Models.Dtos.Reclamo;

namespace BusinessLayer.BLs
{
    public class BL_Compra : IBL_Compra
    {
        private IDAL_Compra _compraIdal;
        private IDAL_EstadoCompra _estadoCompra;
        private IDAL_EnvioPaquete _envioPaquete;

        private readonly IMapper _mapper;
        private IBL_Mail _mail;

        public BL_Compra(IDAL_Compra compraDal, IDAL_EstadoCompra estadoCompra, IDAL_EnvioPaquete envioPaquete, IMapper mapper, UserManager<Usuario> _userManage, IBL_Mail mail)
        {
            _compraIdal = compraDal;
            _estadoCompra = estadoCompra;
            _envioPaquete = envioPaquete;
            _mapper = mapper;
            _mail = mail;
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
                EstadoCompra estado = compra.ComprasEstados.Where(ce => ce.EstadoActual).FirstOrDefault().EstadoCompra;
                result.Add(new SortCompra
                {
                    Id = compra.Id,
                    costoTotal = compra.CostoTotal,
                    user = new SortUserDto { Id = compra.Usuario.Id, Email = compra.Usuario.Email, Imagen = compra.Usuario.Imagen, Nombre = compra.Usuario.Nombre, Tel = compra.Usuario.Celular },
                    metodoEnvio = compra.MetodoEnvio.ToString(),
                    metodoPago = compra.MetodoPago.ToString(),
                    fecha = compra.Fecha,
                    estado = estado.Nombre,
                    cantidadDeProductos = compra.ComprasProductos.Count,
                    estadoId = estado.Id
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
            var userId = compra.Usuario.Id;
            var reclamos = await _compraIdal.getReclamosByCompra(compra.Id, userId);
            List<ReclamoDto> reclamosItems = new List<ReclamoDto>();
            if (reclamos != null && reclamos.Count() > 0)
            {
                foreach (Reclamo reclamo in reclamos)
                {
                    Compra comp = reclamo.Compra;
                    Usuario user = compra.Usuario;


                    SortCompra compraSortDto = new SortCompra { Id = compra.Id, costoTotal = compra.CostoTotal, metodoEnvio = compra.MetodoEnvio.ToString(), metodoPago = compra.MetodoPago.ToString() };
                    SortUserDto userSortDto = new SortUserDto { Id = user.Id, Tel = user.Celular, Email = user.Email, Imagen = user.Imagen, Nombre = user.Nombre };

                    reclamosItems.Add(new ReclamoDto { Id = reclamo.Id, Description = reclamo.Descripcion, Estado = reclamo.EstadoReclamo.ToString(), Fecha = reclamo.CreatedAt, compra = compraSortDto, usuario = userSortDto });
                }
            }

           

            Usuario comprador = compra.Usuario;
            Empresa empresa = compra.Empresa;
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

            List<CompraEstadoDto> estadosDto = new List<CompraEstadoDto>();
            List<EstadoCompra>  estados  = await this._estadoCompra.list();
            foreach (EstadoCompra estado in estados)
            {
                var completado = false;
                DateTime? fecha = null;
                CompraEstado? compraEstado  =  compra.ComprasEstados.Find(ce => ce.EstadoCompraId == estado.Id);
                if(compraEstado != null){
                    completado = true;
                    fecha = compraEstado.Fecha;
                }
                estadosDto.Add(new CompraEstadoDto {Fecha = fecha, Estado = estado.Nombre, EstadoId = estado.Id, Completado = completado });
            }

            EstadoCompra? estadoActual = compra.ComprasEstados.Where(ce => ce.EstadoActual).FirstOrDefault().EstadoCompra;
            CompraDto compraInfo = new CompraDto
            {
                Id = compra.Id,
                CantidadDeProductos = compra.ComprasProductos.Count,
                Estado = estadoActual.Nombre,
                EstadoId = estadoActual.Id,
                MetodoEnvio = compra.MetodoEnvio.ToString(),
                MetodoPago = compra.MetodoPago.ToString(),
                Fecha = compra.Fecha,
                Comprador = new UsuarioDto { Id = comprador.Id, Celular = comprador.Celular, Email = comprador.Email, Imagen = comprador.Imagen, Nombre = comprador.Nombre },
                CostoTotal = compra.CostoTotal,
                Lineas = lineas,
                Empresa = new EmpresaDto {  Id = empresa.Id, Correo = empresa.Correo, CostoEnvio = empresa.CostoEnvio, Direccion = empresa.Direccion, Imagen = empresa.Imagen, Nombre = empresa.Nombre, Telefono = empresa.Telefono, Wallet = empresa.Wallet  },
                HistorialEstados = estadosDto,
                reclamosUsuario = reclamosItems,
                codigoSeguimiento = compra.EnvioPaquete != null ? compra.EnvioPaquete.CodigoSeguimiento : "",

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

        public async Task<EstadoCompraDto> pasarAlSiguienteEstado(int compraId)
        {
          Compra compra =  await _compraIdal.getById(compraId);
          CompraEstado lastEstado = compra.ComprasEstados.Where(ce => ce.EstadoActual).FirstOrDefault();
          if(lastEstado.EstadoCompraId == 5)
          {
                throw new Exception("No puedes pasar al siguiente ya que es el ultimo");

          }
          EstadoCompra nuevoEstado  =  await  _estadoCompra.getById(lastEstado.EstadoCompraId + 1);
            Usuario cliente = compra.Usuario;
          await _compraIdal.agregarEstado(compra.Id, nuevoEstado);
           string path = @"Templates/CompraEstadoNotificacionClient.html";
            string content = File.ReadAllText(path);

            string finalContent = content.Replace("{{clienteNombre}}", cliente.Nombre)
             .Replace("{{codigoCompra}}", compra.Id.ToString())
             .Replace("{{nuevoEstado}}", nuevoEstado.Nombre.ToString())
             .Replace("{{fecha}}", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));

            _mail.sendMail(cliente.Email, "SU COMPRA #"+compra.Id + "HA CAMBIADO AL SIGUIENTE ESTADO", finalContent);

            return new EstadoCompraDto { Id = nuevoEstado.Id, Nombre = nuevoEstado.Nombre };

        }
    }
}
