using acme_backend.Models.Dtos;
using acme_backend.Models;
using AutoMapper;
using acme_backend.Models.Dtos.Departamento;
using acme_backend.Models.Dtos.TipoIVA;
using acme_backend.Models.Dtos.Ciudad;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Direccion, DireccionDTO>();
        CreateMap<TipoIva, TipoIVADTO>();
        CreateMap<Departamento, DepartamentoDTO>();
        CreateMap<Ciudad, CiudadDTO>();

    }
}