using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Ciudad;
using DataAccessLayer.Models.Dtos.Departamento;
using DataAccessLayer.Models.Dtos.Pickup;
using DataAccessLayer.Models.Dtos.TipoIVA;

namespace acme_backend.Shared.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Direccion, DireccionDTO>();
            CreateMap<TipoIva, TipoIVADTO>();
            CreateMap<Departamento, DepartamentoDTO>();
            CreateMap<Ciudad, CiudadDTO>();
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<PickUp, PickupDto>();
            CreateMap<LookAndFeel, LookAndFeelDTO>();
            CreateMap<CategoriaDestacada, CategoriaDestacadaListDTO>();
        }
    }
}
