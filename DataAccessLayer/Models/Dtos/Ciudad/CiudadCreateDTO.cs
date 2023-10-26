
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos.Ciudad
{
    public class CiudadCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public int DepartamentoId { get; set; }
    }
}