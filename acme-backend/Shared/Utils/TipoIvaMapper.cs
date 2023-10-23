using acme_backend.Models;
using AutoMapper;
using acme_backend.Models.Dtos.TipoIVA;

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
