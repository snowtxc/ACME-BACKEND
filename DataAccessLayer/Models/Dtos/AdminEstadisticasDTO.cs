
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class AdminEstadisticasDTO
    {
        public int? ProductosVendidosEsteMes { get; set; } = 0;
        public List<ProductoEstadisticaDTO>? ProductosMasVendidos { get; set; }
        public List<VentasPorMesDTO>? VentasPorMes { get; set; }
        public List<VentasPorEmpresaDTO>? VentasMensualesPorEmpresa { get; set; }
    }
}