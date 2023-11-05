
using DataAccessLayer.Models.Dtos.Pickup;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Pickup
    {
        Task<PickupDto> create(string userLoggedId, PickupCreateDto pickupCreate);
        Task<List<PickupDto>> list(string userLoggedId);
        Task<List<PickupDto>> deletesByIds(int[] pickupsIds);

        Task<List<PickupDto>> listByEmpresa(int empresaId);

    }
}
