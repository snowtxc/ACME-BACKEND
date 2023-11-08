using BusinessLayer.BLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;

namespace BusinessLayer.IBLs
{
    public interface IBL_Users
    {
        public Task<UsuarioListDto> getUserById(string id);
        public Task<List<UsuarioListDto>> listUsers(string userId);
        public Task<UsuarioCreateDto> createUser(string userId, UsuarioCreateDto userDto);
        public Task updateUser(string id, UpdateUsuarioDto userDto);
        public Task deleteUser(string id);
        public Task agregarDireccion(string userId, DireccionDTO direccionDto);
        public Task modificarDireccion(string userId, DireccionDTO direccionDto);
        public Task<List<DireccionDTO>> listLoggUsrDirecciones(string userId);
    }
}
