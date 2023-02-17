using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.Horario;
using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Profiles
{
    public class HorarioProfile : Profile
    {
        public HorarioProfile()
        {
            CreateMap<DiaSemana?, DiaSemana>().ConvertUsing((src, dest) => src ?? dest);


            CreateMap<Horario, HorarioDto>();
            CreateMap<HorarioCreateDto, Horario>(); 
            CreateMap<HorarioUpdateDto, Horario>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null)); ;
        }
    }
}
