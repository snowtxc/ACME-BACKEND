using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IBLs;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLayer.BLs
{
    public class BL_Auth: IBL_Auth
    {
        private IDAL_Auth _auth;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BL_Auth(IDAL_Auth auth, IWebHostEnvironment webHostEnvironment)
        {
            _auth = auth;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<string> createUserWithExternalService(LoginWithCredentialsDTO userInfo)
        {

            var app = FirebaseAppSingleton.GetFirebaseApp(_webHostEnvironment.ContentRootPath);
            var auth = FirebaseAuth.GetAuth(app);

            var decodedToken = await auth.VerifyIdTokenAsync(userInfo.Token);

            // Obtener información del usuario
            string uid = decodedToken.Uid;
            string email = decodedToken.Claims["email"]?.ToString();
            string displayName = decodedToken.Claims["name"]?.ToString();
            string imagen = decodedToken.Claims["picture"]?.ToString();


            var userData = new FirebaseUser();
            userData.Uid = uid;
            userData.Email = email;
            userData.Name = displayName;
            userData.Imagen = imagen;

            return await _auth.createUserWithExternalService(userData);
        }

    }
}
