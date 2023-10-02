using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class Ciudad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string Nombre { get; set; } = "";


        [Required]
        public int DepartamentoId { get; set; }

        [Required]
        public Departamento Departamento { get; set; }
    }
}
