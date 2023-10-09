
namespace acme_backend.Models.Dtos
{
    public class UsuarioCreateDto
    {
        public string? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Celular { get; set; } = string.Empty;
        
        public string Imagen { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int EmpresaId { get; set; }

        public DireccionDTO Direccion { get; set; }
    }
}
