using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Estadisticas: IBL_Estadisticas
    {
        private IDAL_Estadisticas _estadisticas;

        public BL_Estadisticas(IDAL_Estadisticas estad)
        {
            _estadisticas = estad;
        }

        public Task<SortEstadisticasDTO> listarEstadisticas()
        {
            return _estadisticas.listarEstadisticas();
        }

    }
}
