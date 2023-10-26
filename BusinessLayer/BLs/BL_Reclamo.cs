using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Reclamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Reclamo: IBL_Reclamo
    {
        private IDAL_Reclamo _reclamo;

        public BL_Reclamo(IDAL_Reclamo reclamo)
        {
            _reclamo = reclamo;
        }

        public Task<List<ReclamoDto>> list()
        {
            return _reclamo.list();
        }
    }
}
