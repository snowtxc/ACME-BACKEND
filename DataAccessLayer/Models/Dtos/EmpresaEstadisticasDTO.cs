
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class EmpresaEstadisticasDTO
    {

        public int? ProductosVendidosEsteMes { get; set; } = 0;
        public int? ProductosRegistrados { get; set; } = 0;
        public int? UsuariosActivos { get; set; } = 0;
        public List<ProductoEstadisticaDTO>? ProductosMasVendidos { get; set; }
        public List<VentasPorMesDTO>? VentasPorMes { get; set; }
    }
}