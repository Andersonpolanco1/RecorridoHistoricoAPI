using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.EstadoDtos;
using RecorridoHistoricoApi.Utils;
using System.Globalization;

namespace RecorridoHistoricoApi.Profiles
{
    public class EstadoProfile : Profile
    {
        public EstadoProfile() 
        {
            CreateMap<EstadoCreateDto, Estado>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => Util.ToTitleCase(src.Nombre)));

            CreateMap<EstadoDto, Estado>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => Util.ToTitleCase(src.Nombre)));
           
            CreateMap<Estado, EstadoDto>();
        }
    }
}
