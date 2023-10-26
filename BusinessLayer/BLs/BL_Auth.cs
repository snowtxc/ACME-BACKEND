using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IBLs;

namespace BusinessLayer.BLs
{
    public class BL_Auth: IBL_Auth
    {
        private IDAL_Auth _auth;

        public BL_Auth(IDAL_Auth auth)
        {
            _auth = auth;
        }

        public Task<string> generateTokenByUser(Usuario user)
        {
            return _auth.generateTokenByUser(user);
        }

        public Task<string> login(string email, string password)
        {
            return _auth.login(email, password);
        }

        public Task<bool> resetPassword(ResetPasswordInputDTO data)
        {
            return _auth.resetPassword(data);
        }

        public Task<UsuarioDto?> register(UsuarioDto userDto)
        {
            return _auth.register(userDto);
        }

        public Task<bool> forgotPassword(string email)
        {
            return _auth.forgotPassword(email);
        }

        public Task addRoleToUser(string id, string role)
        {
            return _auth.addRoleToUser(id, role);
        }

        public Task<UsuarioDto?> FindByEmail(string email)
        {
            return _auth.FindByEmail(email);
        }

        public Task<UserInfoDTO> getUserInfoById(string userId)
        {
            return _auth.getUserInfoById(userId);
        }

        public Task<string> createUserWithExternalService(LoginWithCredentialsDTO userInfo)
        {
            return _auth.createUserWithExternalService(userInfo);
        }

    }
}
