
namespace DataAccessLayer.Models.Dtos
{
    public class UsuarioDto
    {

        public string? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;


        public string Celular { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string[] Roles { get; set; } = new string[] { };

    }
}
