
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class ProductoEstadisticaDTO
    {
        public int? ProductoId { get; set; } = 0;
        public int? EmpresaId { get; set; } = 0;
        public string? EmpresaNombre { get; set; } = string.Empty;
        public string? ProductoNombre { get; set; }
        public int? CantidadVendida { get; set; } = 0;

    }
}