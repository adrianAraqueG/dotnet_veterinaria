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
        CreateMap<Mascota, MascotaRegDto>().ReverseMap();

        CreateMap<Veterinario, VeterinarioDto>().ReverseMap();

        CreateMap<Laboratorio, LaboratorioDto>().ReverseMap();
        CreateMap<Laboratorio, LaboratorioRegDto>().ReverseMap();
        CreateMap<Laboratorio, LaboratorioSimpleDto>().ReverseMap();
        
        CreateMap<Medicamento, MedicamentoDto>().ReverseMap();
        CreateMap<Medicamento, MedicamentoRegDto>().ReverseMap();

        CreateMap<MedicamentoMovimiento, MedicamentoMovimientoSimpleDto>().ReverseMap();
        CreateMap<MedicamentoMovimiento, MedicamentoMovimientoRegDto>().ReverseMap();

        CreateMap<Tratamiento, TratamientoSimpleDto>().ReverseMap();
        CreateMap<Tratamiento, TratamientoRegDto>().ReverseMap();

        CreateMap<Proveedor, ProveedorSimpleDto>().ReverseMap();
        CreateMap<Proveedor, ProveedorRegDto>().ReverseMap();
        
        CreateMap<Propietario, PropietarioRegDto>().ReverseMap();
        CreateMap<Propietario, PropietarioSimpleDto>().ReverseMap();

        CreateMap<Raza, RazaSimpleDto>().ReverseMap();
        CreateMap<Raza, RazaRegDto>().ReverseMap();

        CreateMap<Especie, EspecieRegDto>().ReverseMap();
        CreateMap<Especie, EspecieSimpleDto>().ReverseMap();

        CreateMap<TipoMovimiento, TipoMovimientoSimpleDto>().ReverseMap();
        CreateMap<TipoMovimiento, TipoMovimientoRegDto>().ReverseMap();


    }

}