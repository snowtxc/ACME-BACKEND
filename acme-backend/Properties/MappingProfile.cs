using acme_backend.Models.Dtos;
using acme_backend.Models;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Direccion, DireccionDTO>();
    }
}