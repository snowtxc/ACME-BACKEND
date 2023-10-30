using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Models
{
    public class Calificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; } = "";

        public int Rate { get; set; } = 0;

        public string Comentario { get; set; } = "";

        public string UsuarioId { get; set; }
        public int ProductoId { get; set; }

        public bool Activo { get; set; } = true;


        [JsonIgnore]
        public Producto Producto { get; set; } = null!;

        [JsonIgnore]
        public Usuario Usuario { get; set; } = null!;
    }
}
