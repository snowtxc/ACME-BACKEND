
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models.Dtos.Ciudad
{
    public class CiudadDTO
    {
        public int? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int DepartamentoId { get; set; } = 0;
    }
}