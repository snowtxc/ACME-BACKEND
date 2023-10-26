using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Estadisticas
    {

        public Task<SortEstadisticasDTO> listarEstadisticas();

    }
}
