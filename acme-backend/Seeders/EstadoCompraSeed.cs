
using DataAccessLayer.Db;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Seeders;

public static class EstadoCompraSeed
{
    public static void SeedData(ApplicationDbContext context)
    {
        AddOrUpdateEstadoCompra(context, EstadoCompraEnum.PendientePreparacion);
        AddOrUpdateEstadoCompra(context, EstadoCompraEnum.PedidoPreparado);
        AddOrUpdateEstadoCompra(context, EstadoCompraEnum.PedidoEnviado);
        AddOrUpdateEstadoCompra(context, EstadoCompraEnum.PedidoListoRetirarSucursal);
        AddOrUpdateEstadoCompra(context, EstadoCompraEnum.PedidoEntregado);
        context.SaveChanges();
    }

    private static void AddOrUpdateEstadoCompra(ApplicationDbContext context, EstadoCompraEnum estadoCompraEnum)
    {
        var estadoCompraNombre = estadoCompraEnum.ToString();

        var existingEstadoCompra = context.EstadosCompras.SingleOrDefault(ec => ec.Nombre == estadoCompraNombre);

        if (existingEstadoCompra == null)
        {
            context.EstadosCompras.Add(new EstadoCompra { Nombre = estadoCompraNombre, Activo = true });
        }
        else
        {
            existingEstadoCompra.Activo = true;
        }
    }
}