using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.Enums;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Envio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<CompraOKDTO> finalizarCarrito(FInalizarCarritoDTO data, string userId)
        {
            var dataToReturn = new CompraOKDTO();
            MetodoEnvio selectedMetodoEnvio = MetodoEnvio.RetiroPickup;
            MetodoPago selectedMetodoPago = MetodoPago.Tarjeta;

            //            List<CompraProducto> comprasProductos = new List<CompraProducto>();
            HttpClient httpClient = new HttpClient();
            var lineasCarrito = await _db.LineasCarrito.Include((p) => p.Producto).Include((p) => p.Producto.TipoIva).Include((p) => p.Producto.Empresa).Where((c) => c.UsuarioId == userId && c.Producto.Empresa.Id == data.EmpresaId).ToListAsync();
            var subtotal = 0.0;
            var empresaInfo = await _db.Empresas.FindAsync(data.EmpresaId);
            Compra compra = null;
            var loggedUserInfo = await _db.Usuarios.FindAsync(userId);
            if (loggedUserInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            if (empresaInfo == null)
            {
                throw new Exception("Empresa invalida");
            }

            foreach (var item in lineasCarrito)
            {
                if (item != null)
                {
                    subtotal = subtotal + ((item.Producto.Precio + ((item.Producto.Precio) * item.Producto.TipoIva.Porcentaje) / 100) * item.Cantidad);
                }
            }
            if (data.MetodoEnvio == 1)
            {
                subtotal += empresaInfo.CostoEnvio;
            }

            var isPaymentOk = false;
            if (data.MetodoPago == 1)
            {
                selectedMetodoPago = MetodoPago.Tarjeta;
                // tarjeta
                string mockPaymentsUrl = "http://payments/api/Payments/processPayment";

                    if (data.PaymentInfo != null)
                    {
                        data.PaymentInfo.Amount = subtotal;
                        string jsonBody = JsonConvert.SerializeObject(data.PaymentInfo);
                        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await httpClient.PostAsync(mockPaymentsUrl, content);
                        if (response.IsSuccessStatusCode)
                        {

                            string responseData = await response.Content.ReadAsStringAsync();
                            PaymentResponse apiResponse = JsonConvert.DeserializeObject<PaymentResponse>(responseData);
                            if (apiResponse == PaymentResponse.OK)
                            {
                                dataToReturn.Ok = true;
                                dataToReturn.Mensaje = "Compra realizada correctamente";
                                isPaymentOk = true;
                            }
                            else if (apiResponse == PaymentResponse.INCORRECT_VERIFICATION_CODE)
                            {
                                dataToReturn.Ok = false;
                                dataToReturn.Mensaje = "Codigo de verificacion invalido";
                                isPaymentOk = false;
                            }
                            else if (apiResponse == PaymentResponse.INSUFFICIENT_BALANCE)
                            {
                                dataToReturn.Ok = false;
                                dataToReturn.Mensaje = "Pago rechazado por financiera , chequea el saldo y vuelve a intentarlo";
                                isPaymentOk = false;
                            }
                            else if (apiResponse == PaymentResponse.EXPIRED_CARD)
                            {
                                dataToReturn.Ok = false;
                                dataToReturn.Mensaje = "Tarjeta invalida";
                                isPaymentOk = false;
                            }
                            else if (apiResponse == PaymentResponse.TIMEOUT)
                            {
                            // Simula una espera de 5 segundos (puedes ajustar este valor)
                                await Task.Delay(TimeSpan.FromSeconds(10));
                                dataToReturn.Ok = false;
                                dataToReturn.Mensaje = "Tiempo de espera expirado";
                                isPaymentOk = false;
                            }

                        }
                        else
                        {
                            throw new Exception("Error al comunicarse con la api de pagos");
                        }
                    } else {
                        throw new Exception("Datos de pago invalidos");
                    }
                   
            }
            else if (data.MetodoPago == 2)
            {
                selectedMetodoPago = MetodoPago.Wallet;

                //validar wallet
            }
            else
            {
                throw new Exception("Metodo de envio invalido");
            }

            if (data.MetodoEnvio == 1)
            {
                selectedMetodoEnvio = MetodoEnvio.DireccionPropia;
            }
            else if (data.MetodoEnvio == 2)
            {
                selectedMetodoEnvio = MetodoEnvio.RetiroPickup;
            }
            else
            {
                throw new Exception("Metodo de envio invalido");
            }

            // generar compra si isPaymentOk es true
            if (isPaymentOk)
            {
                compra = new Compra();
                compra.MetodoEnvio = selectedMetodoEnvio;
                compra.MetodoPago = selectedMetodoPago;
                compra.Usuario = loggedUserInfo;
                compra.Fecha = DateTime.Now;
                compra.CostoTotal = subtotal;
                compra.Empresa = empresaInfo;

                var estadoCompra = await _db.EstadosCompras.Where((d) => d.Nombre == EstadoCompraEnum.PendientePreparacion.ToString()).FirstOrDefaultAsync();
                if (estadoCompra == null)
                {
                    throw new Exception("Estado de compra invalido");
                }
                var compraEstado = new CompraEstado();
                compraEstado.compra = compra;
                compraEstado.EstadoCompra = estadoCompra;
                compraEstado.EstadoActual = true;
                await _db.Compras.AddAsync(compra);
                await _db.ComprasEstados.AddAsync(compraEstado);


                foreach (var item in lineasCarrito)
                {
                    if (item != null)
                    {
                        var compraProd = new CompraProducto();
                        compraProd.Cantidad = item.Cantidad;
                        compraProd.Producto = item.Producto;
                        compraProd.Compra = compra;
                        compraProd.PrecioUnitario = (item.Producto.Precio + ((item.Producto.Precio) * item.Producto.TipoIva.Porcentaje) / 100);
                        await _db.ComprasProductos.AddAsync(compraProd);
                    }
                }

                if (data.MetodoEnvio == 1)
                {
                    // domicilio
                    var direccionSelected = await _db.Direcciones.Include((c) => c.Ciudad).ThenInclude((c) => c.Departamento).FirstOrDefaultAsync((e) => e.Id == data.DireccionSeleccionadaId);
                    if (direccionSelected == null)
                    {
                        throw new Exception("Pickup invalido");
                    }
                    //check paquete
                         var direccionData = new EnvioRequestDto();
                         direccionData.Ciudad = direccionSelected.Ciudad.Nombre;
                         direccionData.CantidadDeProductos = lineasCarrito.Count();
                         direccionData.NroPuerta = direccionSelected.NroPuerta;
                         direccionData.Departamento = direccionSelected.Ciudad.Departamento.Nombre;
                         direccionData.Calle = direccionSelected.Calle;

                         string jsonInfo = JsonConvert.SerializeObject(direccionData);
                         var contenido = new StringContent(jsonInfo, Encoding.UTF8, "application/json");

                         HttpResponseMessage response = await httpClient.PostAsync("http://shipping/api/Shipping/createPackage", contenido);
                         if (response.IsSuccessStatusCode)
                         {
                            string responseData = await response.Content.ReadAsStringAsync();
                            EnvioRastreoResponseDto apiResponse = JsonConvert.DeserializeObject<EnvioRastreoResponseDto>(responseData);
                            var envioPaquete = new EnvioPaquete();
                            if (apiResponse == null)
                            {
                                throw new Exception("Error inesperado");
                            }
                            envioPaquete.FechaEstimadaEntrega = apiResponse.arrivalDate;
                            envioPaquete.Compra = compra;
                            envioPaquete.Direccion = direccionSelected;
                            envioPaquete.CodigoSeguimiento = apiResponse.trackingNumber;
                            await _db.EnvioPaquetes.AddAsync(envioPaquete);
                         }
                         else
                         {
                        Console.WriteLine("Error aca");
                        Console.WriteLine(response.StatusCode);
                             throw new Exception("Error al comunicarse con la api de envios");
                         }
                }
                else if (data.MetodoEnvio == 2)
                {
                    //pickup
                    var pickUpSelected = await _db.Pickups.Include((p) => p.Empresa).Where((p) => p.Id == data.DireccionSeleccionadaId).FirstOrDefaultAsync();
                    if (pickUpSelected == null)
                    {
                        throw new Exception("Pickup invalido");
                    }
                    if (pickUpSelected.Empresa.Id != empresaInfo.Id)
                    {
                        throw new Exception("Pickup invalido");
                    }
                    var retiroPickup = new RetiroPickup();
                    retiroPickup.Entregado = false;
                    double minutesToSum = new Random().Next(1440, 4320);    //minutos a sumar , entre 1440 minutos equivalente a 1 dia y 4320 equivalente a 3 dias
                    DateTime fechaDisponibileRetiro = DateTime.Now.AddMinutes(minutesToSum);
                    retiroPickup.FechaLlegada = fechaDisponibileRetiro;
                    retiroPickup.Compra = compra;
                    retiroPickup.Pickup = pickUpSelected;
                    await _db.RetirosPickup.AddAsync(retiroPickup);
                }
                else
                {
                    throw new Exception("Metodo de envio invalido");
                }

                foreach (var item in lineasCarrito)
                {
                    _db.LineasCarrito.Remove(item);
                }
                // send email
            } else
            {
                dataToReturn.Ok = false;
                throw new Exception(dataToReturn.Mensaje);
            }
            await _db.SaveChangesAsync();
            if (compra != null)
            {
                dataToReturn.compraId = compra.Id.ToString();
            }
            return dataToReturn;
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
