namespace acme_backend.Models.Dtos
{
    public class ResetPasswordInputDTO
    {

        public string Token { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

    }
}
