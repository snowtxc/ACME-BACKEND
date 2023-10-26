

using DataAccessLayer.Models.Dtos.Reclamo;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Reclamo
    {
        Task<List<ReclamoDto>> list();
    }
}
