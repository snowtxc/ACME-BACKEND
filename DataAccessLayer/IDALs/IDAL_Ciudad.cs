using DataAccessLayer.Models.Dtos.Ciudad;


namespace DataAccessLayer.IDALs
{
    public interface IDAL_Ciudad
    {
        Task<CiudadDTO> createCiudad(CiudadCreateDTO ciudad);
        Task<List<CiudadDTO>> listarCiudades();
        Task<CiudadDTO> getCiudadById(int id);
        Task<CiudadDTO> updateCiudad(CiudadDTO ciudad);
        Task deleteCiudad(int id);
        Task<List<CiudadDTO>> listarCiudadesByDepartamento(int departamentoId);
    }
}
