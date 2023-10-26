using DataAccessLayer.Models.Dtos.TipoIVA;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_TipoIVA
    {
        Task<TipoIVADTO> createTipoIva(TipoIVADTO tipoIva);
        Task<List<TipoIVADTO>> listarTiposIVA();
        Task<TipoIvaList> getTipoIvaById(int id);
        Task<TipoIVADTO> updateTipoIva(TipoIVADTO tipoIva);
        Task deleteTipoIva(int id);
    }
}
