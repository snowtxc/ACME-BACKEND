
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class DireccionDTO
    {
        public int? Id { get; set; }

        public string Calle { get; set; } = string.Empty;

        public string NroPuerta { get; set; } = string.Empty;

        public string CalleEntre1 { get; set; } = string.Empty;

        public string CalleEntre2 { get; set; } = string.Empty;

        public int CiudadId { get; set; }
    }
}