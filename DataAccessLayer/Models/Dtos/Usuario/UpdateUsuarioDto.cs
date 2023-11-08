
namespace DataAccessLayer.Models.Dtos
{
    public class UpdateUsuarioDto
    {

        public string? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Celular { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;
    }
}
