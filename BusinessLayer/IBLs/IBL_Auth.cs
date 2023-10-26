using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Auth
    {
        public Task<string> generateTokenByUser(Usuario user);

        public Task<string> login(string email, string password);

        public Task<bool> resetPassword(ResetPasswordInputDTO data);

        public Task<UsuarioDto?> register(UsuarioDto userDto);

        public Task<bool> forgotPassword(string email);

        public Task addRoleToUser(string id, string role);
        public Task<UsuarioDto?> FindByEmail(string email);

        public Task<UserInfoDTO> getUserInfoById(string userId);

        public Task<string> createUserWithExternalService(LoginWithCredentialsDTO userInfo);

    }
}
