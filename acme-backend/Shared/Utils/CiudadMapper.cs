using acme_backend.Models;
using AutoMapper;
using acme_backend.Models.Dtos.Ciudad;

namespace acme_backend.Shared.Utils
{
    public class CiudadMapper
    {
        private readonly IMapper _mapper;

        public CiudadMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CiudadDTO MapCiudadToDto(Ciudad ciudad)
        {
            return _mapper.Map<CiudadDTO>(ciudad);
        }
    }
}
