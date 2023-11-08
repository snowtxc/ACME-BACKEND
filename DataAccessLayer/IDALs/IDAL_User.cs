

using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_User
    {
        Task<UsuarioListDto> getUserById(string id);
        Task<List<UsuarioListDto>> listUsers(string userId);
        Task<UsuarioCreateDto> createUser(string userId, UsuarioCreateDto userDto);
        Task updateUser(string id, UpdateUsuarioDto userDto);
        Task deleteUser(string id);
        Task agregarDireccion(string userId, DireccionDTO direccionDto);
        Task modificarDireccion(string userId, DireccionDTO direccionDto);
        Task<List<DireccionDTO>> listLoggUsrDirecciones(string userId);
    }
}
