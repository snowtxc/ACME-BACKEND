using DataAccessLayer.Models;
using AutoMapper;
using DataAccessLayer.Models.Dtos.Departamento;

namespace DataAccessLayer.Utils
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
