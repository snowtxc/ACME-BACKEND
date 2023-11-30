

using DataAccessLayer.Models.Dtos.Reclamo;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Reclamo
    {
        Task<List<ReclamoDto>> list();

        Task crear(ReclamoCreateDTO data); 
        Task cerrarReclamo(int reclamoId);

    }



}
