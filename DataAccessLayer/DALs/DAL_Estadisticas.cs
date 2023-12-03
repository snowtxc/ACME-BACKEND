using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.Enums;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataAccessLayer.IDALs
{
    public class DAL_Estadisticas : IDAL_Estadisticas
    {

        private ApplicationDbContext _db;
        private RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;

        private readonly IMapper _mapper;

        public DAL_Estadisticas(ApplicationDbContext db, IMapper mapper, RoleManager<IdentityRole> roleMan, UserManager<Usuario> userManag)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManag;
            _roleManager = roleMan;
        }

        public async Task<SortEstadisticasDTO> listarEstadisticas()
        {
            var estadisticas = new SortEstadisticasDTO();
            var empresas = _db.Empresas.Where(e => e.Activo == true).ToList();
            var userRole = await _roleManager.FindByNameAsync("Usuario");
            if (userRole == null)
            {
                throw new Exception("Rol invalido");
            }
            var usuariosUsers = await _userManager.GetUsersInRoleAsync(userRole.Name);
            var compras = await _db.Compras.Where(c => c.Activo == true).ToListAsync();

            estadisticas.EmpresasActivas = empresas.Count;
            estadisticas.UsuariosActivos = usuariosUsers.Count;
            estadisticas.ProductosVendidos = compras.Count;

            return estadisticas;
        }

        public async Task<AdminEstadisticasDTO> listarEstadisticasAdmin()
        {
            var adminStats = new AdminEstadisticasDTO();

            var fechaInicioMesPasado = DateTime.Now.AddMonths(-1).Date;

            var comprasUltimoMes = await _db.Compras
                .Include(c => c.ComprasProductos)
                .ThenInclude(compraProducto => compraProducto.Producto)
                .ThenInclude(producto => producto.Empresa)
                .Where(c => c.Fecha >= fechaInicioMesPasado && c.Activo == true)
                .ToListAsync();

            // top mas vendidos este mes
            var productosMasVendidos = comprasUltimoMes
                .SelectMany(c => c.ComprasProductos)
                .GroupBy(cp => cp.Producto)
                .Select(g => new ProductoEstadisticaDTO
                {
                    ProductoId = g.Key.Id,
                    EmpresaId = g.Key.Empresa.Id,
                    EmpresaNombre = g.Key.Empresa.Nombre,
                    ProductoNombre = g.Key.Titulo,
                    CantidadVendida = g.Sum(cp => cp.Cantidad)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .Take(5)
                .ToList();


            var ventasPorMes = new List<VentasPorMesDTO>();
            var fechaInicioAnioActual = new DateTime(DateTime.Now.Year, 1, 1);

            var comprasAnioActual = await _db.Compras
                .Where(c => c.Fecha >= fechaInicioAnioActual && c.Activo == true)
                .ToListAsync();

            var todosLosMeses = Enumerable.Range(1, 12)
                .Select(mes => new VentasPorMesDTO
                {
                    Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(mes),
                    CantidadVentas = 0
                })
                .ToList();

            var ventasAgrupadas = comprasAnioActual
                .GroupBy(c => c.Fecha.Month)
                .Select(g => new VentasPorMesDTO
                {
                    Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(g.Key),
                    CantidadVentas = g.Count()
                })
                .ToList();

            var allMonthsOrders = todosLosMeses
                .GroupJoin(ventasAgrupadas,
                    mesCompleto => mesCompleto.Mes,
                    ventaMes => ventaMes.Mes,
                    (mesCompleto, ventas) => new VentasPorMesDTO
                    {
                        Mes = mesCompleto.Mes,
                        CantidadVentas = ventas.Sum(v => v.CantidadVentas)
                    })
                .ToList();

            var ventasPorEmpresa = await _db.Empresas
                .Where(empresa => _db.Compras
                    .Include(compra => compra.Empresa)
                    .Any(c => c.EmpresaId == empresa.Id &&
                              c.Fecha.Month == DateTime.Now.Month &&
                              c.Fecha.Year == DateTime.Now.Year &&
                              c.Activo == true))
                .Select(empresa => new VentasPorEmpresaDTO
                {
                    EmpresaId = empresa.Id,
                    EmpresaNombre = empresa.Nombre,
                    CantidadVentasMesActual = _db.Compras
                        .Count(c => c.EmpresaId == empresa.Id &&
                                    c.Fecha.Month == DateTime.Now.Month &&
                                    c.Fecha.Year == DateTime.Now.Year &&
                                    c.Activo == true)
                })
                .ToListAsync();

            adminStats.ProductosVendidosEsteMes = comprasUltimoMes.Sum(c => c.ComprasProductos.Count);
            adminStats.ProductosMasVendidos = productosMasVendidos;
            adminStats.VentasPorMes = allMonthsOrders;
            adminStats.VentasMensualesPorEmpresa = ventasPorEmpresa;
            return adminStats;
        }

        public async Task<EmpresaEstadisticasDTO> listarEstadisticasEmpresa(int empresaId)
        {
            var empresaStats = new EmpresaEstadisticasDTO();

            var fechaActual = DateTime.Now.Date;
            var fechaInicioMesPasado = DateTime.Now.AddMonths(-1).Date;
            var fechaInicioAnioActual = new DateTime(DateTime.Now.Year, 1, 1);
            var fechaInicioSemanaActual = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date;

            var comprasUltimoMes = await _db.Compras
                .Include(c => c.ComprasProductos)
                .ThenInclude(compraProducto => compraProducto.Producto)
                .ThenInclude(producto => producto.Empresa)
                .Where(c => c.Activo == true && c.Fecha >= fechaInicioMesPasado && c.EmpresaId == empresaId)
                .ToListAsync();

            // top mas vendidos este mes
            var productosMasVendidos = comprasUltimoMes
                .SelectMany(c => c.ComprasProductos)
                .GroupBy(cp => cp.Producto)
                .Select(g => new ProductoEstadisticaDTO
                {
                    ProductoId = g.Key.Id,
                    EmpresaId = g.Key.Empresa.Id,
                    EmpresaNombre = g.Key.Empresa.Nombre,
                    ProductoNombre = g.Key.Titulo,
                    CantidadVendida = g.Sum(cp => cp.Cantidad)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .Take(5)
                .ToList();


            var ventasPorMes = new List<VentasPorMesDTO>();

            var comprasAnioActual = await _db.Compras
                .Include(c => c.Empresa)
                .Where(c => c.Activo == true && c.Fecha >= fechaInicioAnioActual && c.EmpresaId == empresaId)
                .ToListAsync();

            // todas las compras activas de la empresa, para hhacer el count de los metodos de pago.
            var comprasEmpresa = await _db.Compras
                .Where(c => c.Activo == true && c.EmpresaId == empresaId)
                .ToListAsync();

            var metodosPagoDTO = new MetodosPagoPreferidosDTO();
            var metodosEnvioPrefDTO = new MetodosEnvioPreferidosDTO();

            foreach (var compra in comprasEmpresa)
            {
                switch (compra.MetodoPago)
                {
                    case MetodoPago.MercadoPago:
                        metodosPagoDTO.MercadoPago++;
                        break;
                    case MetodoPago.Tarjeta:
                        metodosPagoDTO.Tarjeta++;
                        break;
                    case MetodoPago.Wallet:
                        metodosPagoDTO.Wallet++;
                        break;
                }

                switch (compra.MetodoEnvio)
                {
                    case MetodoEnvio.DireccionPropia:
                        metodosEnvioPrefDTO.DireccionPropia++;
                        break;
                    case MetodoEnvio.RetiroPickup:
                        metodosEnvioPrefDTO.RetiroPickup++;
                        break;
                }
            }

            var todosLosMeses = Enumerable.Range(1, 12)
                .Select(mes => new VentasPorMesDTO
                {
                    Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(mes),
                    CantidadVentas = 0
                })
                .ToList();

            var ventasAgrupadas = comprasAnioActual
                .GroupBy(c => c.Fecha.Month)
                .Select(g => new VentasPorMesDTO
                {
                    Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(g.Key),
                    CantidadVentas = g.Count()
                })
                .ToList();

            var allMonthsOrders = todosLosMeses
                .GroupJoin(ventasAgrupadas,
                    mesCompleto => mesCompleto.Mes,
                    ventaMes => ventaMes.Mes,
                    (mesCompleto, ventas) => new VentasPorMesDTO
                    {
                        Mes = mesCompleto.Mes,
                        CantidadVentas = ventas.Sum(v => v.CantidadVentas)
                    })
                .ToList();

            // acá obtengo las fechas de los últimos 7 dias, incluyendo hoy
            var fechasDiasSemanaActual = Enumerable.Range(0, 7)
                .Select(diasAtras => fechaActual.AddDays(-diasAtras))
                .OrderBy(fecha => fecha)
                .ToList();

            var ventasAgrupadasPorDia = fechasDiasSemanaActual
                .GroupJoin(comprasUltimoMes,
                    dia => dia,
                    compra => compra.Fecha.Date,
                    (dia, ventas) => new VentasPorDiaDTO
                    {
                        Dia = $"{dia.ToString("dddd", CultureInfo.GetCultureInfo("es-ES"))} - {dia.Day}/{dia.Month:D2}",
                        CantidadVentas = ventas.Count()
                    })
                .ToList();

            var productosRegistrados = _db.Productos
                .Include(p => p.Empresa)
                .Where(p => p.Activo == true && p.Empresa.Id == empresaId)
                .ToList();

            var usuariosActivos = _db.Usuarios
                .Include(u => u.Empresa)
                .Where(u => u.Activo == true && u.EmpresaId == empresaId)
                .ToList();


            // contadores
            empresaStats.ProductosRegistrados = productosRegistrados.Count();
            empresaStats.UsuariosActivos = usuariosActivos.Count();
            empresaStats.ProductosVendidosEsteMes = comprasUltimoMes
                .SelectMany(c => c.ComprasProductos)
                .Sum(cp => cp.Cantidad);
            empresaStats.MetodosPagoPreferidos = metodosPagoDTO;
            empresaStats.MetodosEnvioPreferidos = metodosEnvioPrefDTO;


            // estadísticas complejas
            empresaStats.ProductosMasVendidos = productosMasVendidos;
            empresaStats.VentasPorMes = allMonthsOrders;
            empresaStats.VentasUltimaSemana = ventasAgrupadasPorDia;
            return empresaStats;
        }
    }
}
