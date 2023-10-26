using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.TipoIVA;

namespace acme_backend.Shared.Utils
{
    public class TipoIvaMapper
    {
        private readonly IMapper _mapper;

        public TipoIvaMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TipoIVADTO MapTipoIvaToDto(TipoIva tipoIva)
        {
            return _mapper.Map<TipoIVADTO>(tipoIva);
        }
    }
}
