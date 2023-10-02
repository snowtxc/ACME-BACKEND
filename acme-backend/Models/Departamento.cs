using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string Nombre { get; set; } = "";


      
        public ICollection<Ciudad> Ciudades { get; } = new List<Ciudad>();

    }
}
