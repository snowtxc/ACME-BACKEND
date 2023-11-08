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
        public Task updateUser(string id, UpdateUsuarioDto userDto)
        {
            return _users.updateUser(id, userDto);
        }
        public Task deleteUser(string id)
        {
            return _users.deleteUser(id);
        }
        public Task agregarDireccion(string userId, DireccionDTO direccionDto)
        {
            return _users.agregarDireccion(userId, direccionDto);
        }
        public Task modificarDireccion(string userId, DireccionDTO direccionDto)
        {
            return _users.modificarDireccion(userId, direccionDto);
        }
        public Task<List<DireccionDTO>> listLoggUsrDirecciones(string userId)
        {
            return _users.listLoggUsrDirecciones(userId);
        }
    }
}
