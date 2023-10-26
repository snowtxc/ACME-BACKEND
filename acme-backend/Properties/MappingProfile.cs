using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using DataAccessLayer.Models.Dtos.Ciudad;
using DataAccessLayer.Models.Dtos.Departamento;
using DataAccessLayer.Models.Dtos.TipoIVA;

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