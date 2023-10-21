using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Models.Dtos.Pickup;
using AutoMapper;

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
