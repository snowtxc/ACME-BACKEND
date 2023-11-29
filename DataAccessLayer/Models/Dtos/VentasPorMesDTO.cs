
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class VentasPorMesDTO
    {
        public string? Mes { get; set; } = string.Empty;
        public int? CantidadVentas { get; set; } = 0;
    }
}