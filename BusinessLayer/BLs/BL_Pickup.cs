using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Pickup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Pickup: IBL_Pickup
    {
        private IDAL_Pickup _pickup;

        public BL_Pickup(IDAL_Pickup pickup)
        {
            _pickup = pickup;
        }

        public Task<PickupDto> create(string userLoggedId, PickupCreateDto pickupCreate)
        {
            return _pickup.create(userLoggedId, pickupCreate);
        }
        public Task<List<PickupDto>> list(string userLoggedId)
        {
            return _pickup.list(userLoggedId);
        }
        public Task<List<PickupDto>> deletesByIds(int[] pickupsIds) {
            return _pickup.deletesByIds(pickupsIds);
        }
    }
}
