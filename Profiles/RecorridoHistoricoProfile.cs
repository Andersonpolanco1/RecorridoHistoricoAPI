using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico;
using RecorridoHistoricoApi.Utils;
using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Profiles
{
    public class RecorridoHistoricoProfile : Profile
    {
        public RecorridoHistoricoProfile()
        {
            CreateMap<RecorridoHistorico, RecorridoReadDto>(); 
            CreateMap<RecorridoHistorico, RecorridoHistoricoDetailsDto>(); 

            CreateMap<RecorridoCreateDto, RecorridoHistorico>()
                .ForMember(dest => dest.Telefono, opts => opts.MapFrom(dest => Util.OnlyDigits(dest.Telefono)))
                .ForMember(dest => dest.Cedula, opts => opts.MapFrom(dest => Util.OnlyDigits(dest.Cedula)))
;

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<RecorridoUpdateDto, RecorridoHistorico>()
                .ForMember(dest => dest.Telefono, 
                opts => opts.MapFrom(dest => Util.OnlyDigits(dest.Telefono)))

                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null)); 
        }
    }
}
