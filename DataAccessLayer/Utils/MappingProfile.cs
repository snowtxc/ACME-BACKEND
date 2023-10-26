using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models;
using AutoMapper;
using DataAccessLayer.Models.Dtos.Departamento;
using DataAccessLayer.Models.Dtos.TipoIVA;
using DataAccessLayer.Models.Dtos.Ciudad;

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