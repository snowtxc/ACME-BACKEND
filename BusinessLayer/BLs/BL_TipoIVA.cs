using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.TipoIVA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_TipoIVA: IBL_TipoIVA
    {
        private IDAL_TipoIVA _tipoIva;

        public BL_TipoIVA(IDAL_TipoIVA tipoIva)
        {
            _tipoIva = tipoIva;
        }

        public Task<TipoIVADTO> createTipoIva(TipoIVADTO tipoIva)
        {
            return _tipoIva.createTipoIva(tipoIva);
        }
        public Task<List<TipoIVADTO>> listarTiposIVA()
        {
            return _tipoIva.listarTiposIVA();
        }
        public Task<TipoIvaList> getTipoIvaById(int id)
        {
            return _tipoIva.getTipoIvaById(id);
        }
        public Task<TipoIVADTO> updateTipoIva(TipoIVADTO tipoIva)
        {
            return _tipoIva.updateTipoIva(tipoIva);
        }
        public Task deleteTipoIva(int id)
        {
            return _tipoIva.deleteTipoIva(id);
        }
    }
}
