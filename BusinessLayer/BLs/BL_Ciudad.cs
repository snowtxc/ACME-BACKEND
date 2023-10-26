using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Ciudad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Ciudad: IBL_Ciudad
    {
        private IDAL_Ciudad _ciu;

        public BL_Ciudad(IDAL_Ciudad ciu)
        {
            _ciu = ciu;
        }

        public Task<CiudadDTO> createCiudad(CiudadCreateDTO ciudad)
        {
            return _ciu.createCiudad(ciudad);
        }
        public Task<List<CiudadDTO>> listarCiudades()
        {
            return _ciu.listarCiudades();
        }
        public Task<CiudadDTO> getCiudadById(int id)
        {
            return _ciu.getCiudadById(id);
        }
        public Task<CiudadDTO> updateCiudad(CiudadDTO ciudad)
        {
            return _ciu.updateCiudad(ciudad);
        }
        public Task deleteCiudad(int id)
        {
            return _ciu.deleteCiudad(id);
        }
        public Task<List<CiudadDTO>> listarCiudadesByDepartamento(int departamentoId)
        {
            return _ciu.listarCiudadesByDepartamento(departamentoId);
        }
    }
}
