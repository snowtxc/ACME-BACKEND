
using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;

namespace DataAccessLayer.Utils
{
    public class LookAndFeelMapper
    {
        private readonly IMapper _mapper;
        public LookAndFeelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public LookAndFeelDTO MapLookAndFeelToDto(LookAndFeel laf)
        {
            return _mapper.Map<LookAndFeelDTO>(laf);
        }
    }
}
