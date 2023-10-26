using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Pickup;

namespace acme_backend.Shared.Utils
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
