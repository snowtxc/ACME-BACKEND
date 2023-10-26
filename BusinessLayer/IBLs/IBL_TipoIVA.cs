using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos.TipoIVA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_TipoIVA
    {

        public Task<TipoIVADTO> createTipoIva(TipoIVADTO tipoIva);
        public Task<List<TipoIVADTO>> listarTiposIVA();
        public Task<TipoIvaList> getTipoIvaById(int id);
        public Task<TipoIVADTO> updateTipoIva(TipoIVADTO tipoIva);
        public Task deleteTipoIva(int id);
    }
}
