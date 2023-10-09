using acme_backend.Models.Dtos;
using acme_backend.Models;
using AutoMapper;

namespace acme_backend.Shared.Utils
{
    public class DireccionMapper
    {
        private readonly IMapper _mapper;

        public DireccionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DireccionDTO MapDireccionToDto(Direccion direccion)
        {
            return _mapper.Map<DireccionDTO>(direccion);
        }
    }
}
