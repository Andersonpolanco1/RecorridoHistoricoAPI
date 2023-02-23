using AutoMapper;
using RecorridoHistoricoApi.Models.DTOs.DisponibilidadesDto;

namespace RecorridoHistoricoApi.Profiles
{
    public class DisponibilidadesProfile : Profile
    {
        public DisponibilidadesProfile()
        {
            CreateMap<FechasNoDisponibles,FechasNoDisponiblesDto>()
                .ForMember(dest => dest.ManualesTemporales, 
                opts => opts.MapFrom(src => src.ManualesTemporales.Select(f => f.ToShortDateString())))

                .ForMember(dest => dest.ManualesRecurrentes,
                opts => opts.MapFrom(src => src.ManualesRecurrentes.Select(f => f.ToShortDateString())))
            
                .ForMember(dest => dest.Llenas,
                opts => opts.MapFrom(src => src.Llenas.Select(f => f.ToShortDateString()))); 
        }
    }
}
