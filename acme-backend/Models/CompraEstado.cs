using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace acme_backend.Models
{
    public class CompraEstado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CompraId { get; set; }
        public int EstadoCompraId { get; set; }


        [JsonIgnore]
        public Compra Compra { get; set; } = null!;

        [JsonIgnore]
        public EstadoCompra EstadoCompra { get; set; } = null!;
    }
}
