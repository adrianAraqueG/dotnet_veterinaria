using System.Security.Cryptography.X509Certificates;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {
        CreateMap<Cita, CitaDto>().ReverseMap();
        CreateMap<Cita, CitaRegDto>().ReverseMap();
        
        CreateMap<Mascota, MascotaDto>().ReverseMap();
        CreateMap<Mascota, MascotaSimpleDto>().ReverseMap();

        CreateMap<Veterinario, VeterinarioDto>().ReverseMap();
    }

}