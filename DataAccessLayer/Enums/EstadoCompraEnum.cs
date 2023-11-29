using System.ComponentModel;
namespace DataAccessLayer.Enums
{
    public enum EstadoCompraEnum
    {
        [Description("Pendiente de Preparación")]
        PendientePreparacion,

        [Description("Pedido preparado")]
        PedidoPreparado,

        [Description("Pedido enviado")]
        PedidoEnviado,

        [Description("Pedido listo para retirar en sucursal")]
        PedidoListoRetirarSucursal,

        [Description("PedidoEntregado")]
        PedidoEntregado,
    }
}