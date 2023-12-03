
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class VentasPorDiaDTO
    {
        public string? Dia { get; set; } = string.Empty;
        public int? CantidadVentas { get; set; } = 0;
    }
}