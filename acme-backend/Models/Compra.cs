using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using acme_backend.Shared.Enums;
using System.Text.Json.Serialization;

namespace acme_backend.Models
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public double CostoTotal { get; set; } = 0;

        [Required]
        public MetodoPago MetodoPago { get; set; }

        [Required]
        public MetodoEnvio MetodoEnvio { get; set; }


        public int? EnvioPaqueteId { get; set; } = null;

        public EnvioPaquete? EnvioPaquete { get; set; } = null!;



        public List<EstadoCompra> Estados { get; } = new();

        [JsonIgnore]
        public List<CompraEstado> ComprasEstados{ get; set; } = new();


        public List<Reclamo> Reclamos { get; set; } = new();



        public List<Producto> Productos { get; set; } = new();
        public List<CompraProducto> ComprasProductos { get; set; } = new();


    }
}
