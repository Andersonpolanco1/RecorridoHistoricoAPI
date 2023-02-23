using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico;
using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Profiles
{
    public class RecorridoHistoricoProfile : Profile
    {
        public RecorridoHistoricoProfile()
        {
            CreateMap<RecorridoCreateDto, RecorridoHistorico>();
            CreateMap<RecorridoHistorico, RecorridoReadDto>(); 
            CreateMap<RecorridoHistorico, RecorridoHistoricoDetailsDto>(); 

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<RecorridoUpdateDto, RecorridoHistorico>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null)); 
        }
    }
}
