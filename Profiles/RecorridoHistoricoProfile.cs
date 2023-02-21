using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.RecorridoHistorico;
using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Profiles
{
    public class RecorridoHistoricoProfile : Profile
    {
        public RecorridoHistoricoProfile()
        {
            CreateMap<RecorridoCreateDto, RecorridoHistorico>(); 
            CreateMap<RecorridoHistorico, RecorridoReadDto>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<RecorridoUpdateDto, RecorridoHistorico>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null)); 
        }
    }
}
