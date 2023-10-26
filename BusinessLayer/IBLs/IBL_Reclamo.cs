using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.Reclamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Reclamo
    {

        public Task<List<ReclamoDto>> list();
    }
}
