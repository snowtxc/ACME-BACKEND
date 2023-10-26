using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Direccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Calle { get; set; } = "";

        [Required]
        public string NroPuerta { get; set; } = "";

        public string? CalleEntre1 { get; set; } = null;

        public string? CalleEntre2 { get; set; } = null;


        public int CiudadId { get; set; }

        public Ciudad Ciudad { get; set; } = null!;

    }
}
