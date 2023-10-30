
using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.Utils
{
    public class CategoriaDestacadaMapper
    {
        private readonly IMapper _mapper;
        public CategoriaDestacadaMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CategoriaDestacadaListDTO MapCategoriaDestacadaToDto(CategoriaDestacada cd)
        {
            return _mapper.Map<CategoriaDestacadaListDTO>(cd);
        }
    }
}
