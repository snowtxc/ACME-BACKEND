
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class VentasPorEmpresaDTO
    {
        public int? EmpresaId { get; set; } = 0;
        public string? EmpresaNombre { get; set; } = string.Empty;
        public int? CantidadVentasMesActual { get; set; } = 0;

    }
}