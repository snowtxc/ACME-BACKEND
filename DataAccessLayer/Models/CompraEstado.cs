using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Models
{
    public class CompraEstado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CompraId { get; set; }

        [JsonIgnore]
        public Compra compra { get; set; }

        public int EstadoCompraId { get; set; }

        public bool Activo { get; set; } = true;

        public bool EstadoActual { get; set; } = true;

        public DateTime Fecha { get; set; } = DateTime.Now;


        [JsonIgnore]
        public EstadoCompra EstadoCompra { get; set; } = null!;
    }
}
