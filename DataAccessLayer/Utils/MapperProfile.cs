using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Pickup;
using AutoMapper;

namespace DataAccessLayer.Utils
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<PickUp, PickupDto>();



        }

    }
}
