
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class MetodosEnvioPreferidosDTO
    {
        public int DireccionPropia { get; set; } = 0;
        public int RetiroPickup { get; set; } = 0;
    }
}