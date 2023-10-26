namespace DataAccessLayer.Models.Dtos.Empresa
{
    public class EmpresaCreateDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public string Telefono { get; set; } = "";

        public string Direccion { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Imagen { get; set; } = "";

        public double CostoEnvio { get; set; } = 0;

        public string Wallet { get; set; } = "";

        public string EmailUsuarioAdmin { get; set; } = "";


        public string TelefonoUsuarioAdmin { get; set; } = "";

        public string NombreUsuarioAdmin { get; set; } = "";
    }
}
