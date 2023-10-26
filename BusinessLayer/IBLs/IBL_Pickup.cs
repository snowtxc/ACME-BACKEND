using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Pickup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Pickup
    {
        public Task<PickupDto> create(string userLoggedId, PickupCreateDto pickupCreate);
        public Task<List<PickupDto>> list(string userLoggedId);
        public Task<List<PickupDto>> deletesByIds(int[] pickupsIds);
    }
}
