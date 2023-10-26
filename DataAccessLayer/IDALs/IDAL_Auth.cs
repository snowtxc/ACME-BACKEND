using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Auth
    {
        Task<string> generateTokenByUser(Usuario user);
        Task<string> login(string email, string password);
        Task<bool> resetPassword(ResetPasswordInputDTO data);
        Task<UsuarioDto?> register(UsuarioDto userDto);
        Task<bool> forgotPassword(string email);
        Task addRoleToUser(string id, string role);
        Task<UsuarioDto?> FindByEmail(string email);
        Task<UserInfoDTO> getUserInfoById(string userId);
        Task<string> createUserWithExternalService(LoginWithCredentialsDTO userInfo);
    }
}
