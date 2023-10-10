namespace acme_backend.Models.Dtos
{
    public class LoginWithCredentialsDTO
    {
        public string? Email { get; set; } = "";
        public string? Imagen { get; set; } = "";

        public string Name { get; set; } = "";
        public string Uid { get; set; } = "";

        public string SecretWord { get; set; } = "";

    }
}
