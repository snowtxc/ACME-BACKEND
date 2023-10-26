using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;

namespace BusinessLayer.BLs
{
    public class BL_Users: IBL_Users
    {
        private IDAL_User _users;

        public BL_Users(IDAL_User users)
        {
            _users = users;
        }

        public Task<UsuarioListDto> getUserById(string id)
        {
            return _users.getUserById(id);
        }
        public Task<List<UsuarioListDto>> listUsers(string userId)
        {
            return _users.listUsers(userId);
        }
        public Task<UsuarioCreateDto> createUser(string userId, UsuarioCreateDto userDto)
        {
            return _users.createUser(userId, userDto);
        }
        public Task updateUser(string id, UsuarioDto userDto)
        {
            return _users.updateUser(id, userDto);
        }
        public Task deleteUser(string id)
        {
            return _users.deleteUser(id);
        }
    }
}
