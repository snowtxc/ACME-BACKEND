using DataAccessLayer.Models.Dtos.Ciudad;

namespace BusinessLayer.IBLs
{
    public interface IBL_Ciudad
    {

        public Task<CiudadDTO> createCiudad(CiudadCreateDTO ciudad);
        public Task<List<CiudadDTO>> listarCiudades();
        public Task<CiudadDTO> getCiudadById(int id);
        public Task<CiudadDTO> updateCiudad(CiudadDTO ciudad);
        public Task deleteCiudad(int id);
        public Task<List<CiudadDTO>> listarCiudadesByDepartamento(int departamentoId);
    }
}
