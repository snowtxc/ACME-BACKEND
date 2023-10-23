using acme_backend.Models;
using AutoMapper;
using acme_backend.Models.Dtos.Departamento;

namespace acme_backend.Shared.Utils
{
    public class DepartamentoMapper
    {
        private readonly IMapper _mapper;

        public DepartamentoMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DepartamentoDTO MapDepartamentoToDto(Departamento depto)
        {
            return _mapper.Map<DepartamentoDTO>(depto);
        }
    }
}
