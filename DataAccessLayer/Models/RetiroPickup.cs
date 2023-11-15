using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class RetiroPickup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        [Required]
        public bool Entregado { get; set; } = false;

        [Column]
        [Required]
        public DateTime FechaLlegada { get; set; }

        public Compra Compra { get; set; } = null;

        public PickUp Pickup { get; set; } = null;

    }
}
