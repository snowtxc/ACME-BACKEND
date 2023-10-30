
using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.Utils
{
    public class CategoriaMapper
    {
        private readonly IMapper _mapper;
        public CategoriaMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CategoriaDTO MapCategoriaToDto(Categoria c)
        {
            return _mapper.Map<CategoriaDTO>(c);
        }
    }
}
